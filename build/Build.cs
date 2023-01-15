using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Generators;
using Generators.PathDataGenerators;
using Meta;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[ShutdownDotNetAfterServerBuild]
[SuppressMessage("ReSharper", "UnusedMember.Local")]
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

            var destinationPath = Solution.Directory / "Material.Icons";
            Log.Information("Writing icons meta information to {DestinationPath}", destinationPath);
            
            MaterialIconKindEnumGenerator.Write(destinationPath, iconInfos);
            MaterialIconDataDeclarationGenerator.Write(destinationPath);
            Switch8uStringsGenerator.Write(destinationPath, iconInfos);
        })
        .Triggers(Compile);
}
