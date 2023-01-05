using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Material.Icons;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild {
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
            { });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target UpdateIcons => _ => _
        .Executes(async () =>
        {
            Log.Information("Downloading icons meta information");
            var iconsEnumerable = await MaterialIconsMetaTools.GetIcons();
            var iconInfos = iconsEnumerable.ToList();

            var destinationPath = Solution.Directory / "Material.Icons" / "Generated";
            Log.Information("Writing icons meta information to {DestinationPath}", destinationPath);

            Log.Information("Writing kinds enum");
            File.WriteAllText(Path.Combine(destinationPath, "MaterialIconKind.cs"), GenerateIconKinds(iconInfos));

            Log.Information("Writing data factory");
            File.WriteAllText(Path.Combine(destinationPath, "MaterialIconDataFactory.cs"), GenerateDataFactory(iconInfos));
        })
        .Triggers(Compile);

    public static string GenerateIconKinds(IEnumerable<MaterialIconInfo> materialIconInfos) {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("namespace Material.Icons;");
        stringBuilder.AppendLine("/// ******************************************");
        stringBuilder.AppendLine("/// This code is auto generated. Do not amend.");
        stringBuilder.AppendLine("/// ******************************************");
        stringBuilder.AppendLine("/// <summary>");
        stringBuilder.AppendLine("/// List of available icons.");
        stringBuilder.AppendLine("/// </summary>");
        stringBuilder.AppendLine("/// <remarks>");
        stringBuilder.AppendLine("/// All icons sourced from Material Design Icons Font - https://materialdesignicons.com/ - in accordance of ");
        stringBuilder.AppendLine("/// https://github.com/Templarian/MaterialDesign/blob/master/LICENSE.");
        stringBuilder.AppendLine("/// </remarks>");
        stringBuilder.AppendLine("public enum MaterialIconKind {");

        foreach (var materialIcon in materialIconInfos) {
            stringBuilder.AppendLine($"    {materialIcon.Name},");
            foreach (var iconAlias in materialIcon.Aliases) {
                stringBuilder.AppendLine($"    {iconAlias}={materialIcon.Name},");
            }
        }

        stringBuilder.AppendLine("}");

        return stringBuilder.ToString();
    }

    public static string GenerateDataFactory(IEnumerable<MaterialIconInfo> materialIconInfos) {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("using System;");
        stringBuilder.AppendLine("using System.Collections.Generic;");
        stringBuilder.AppendLine("using System.Collections.ObjectModel;");
        stringBuilder.AppendLine("using Material.Icons;");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("namespace Material.Icons;");
        stringBuilder.AppendLine("/// ******************************************");
        stringBuilder.AppendLine("/// This code is auto generated. Do not amend.");
        stringBuilder.AppendLine("/// ******************************************");
        stringBuilder.AppendLine("public static partial class MaterialIconDataFactory {");
        stringBuilder.AppendLine("    public static IReadOnlyDictionary<MaterialIconKind, string> IconPaths { get; } = new ReadOnlyDictionary<MaterialIconKind, string>(DataSetCreate());");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("    [Obsolete($\"Use {nameof(IconPaths)} instead. This left only for backward compability.\")]");
        stringBuilder.AppendLine("    public static IDictionary<MaterialIconKind, string> DataSetCreate() => new Dictionary<MaterialIconKind, string> {");

        foreach (var info in materialIconInfos) {
            stringBuilder.AppendLine($"        {{MaterialIconKind.{info.Name}, \"{info.Data}\"}},");
        }

        stringBuilder.AppendLine("    };");
        stringBuilder.AppendLine("}");

        return stringBuilder.ToString();
    }
}