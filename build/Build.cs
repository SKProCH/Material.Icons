using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Generators;
using Generators.PathDataGenerators;
using Meta;
using Microsoft.Build.Evaluation;
using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Git;
using Nuke.Common.Utilities.Collections;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
// ReSharper disable InconsistentNaming

[ShutdownDotNetAfterServerBuild]
[SuppressMessage("ReSharper", "UnusedMember.Local")]
partial class Build : NukeBuild {
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("Should we skip compilation of WPF projects. In non windows systems this is enabled by default")]
    readonly bool SkipWpfCompilation = false;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    Target Restore => _ => _
        .Executes(() => {
            GetBuildTargets().ForEach(ExecuteRestoreFor);
            void ExecuteRestoreFor(string projectFile) {
                DotNetRestore(s => s
                    .SetProjectFile(projectFile));
            }
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() => {
            GetBuildTargets().ForEach(ExecuteBuildFor);
            void ExecuteBuildFor(string projectFile) {
                DotNetBuild(s => s
                    .SetProjectFile(projectFile)
                    .SetConfiguration(Configuration)
                    .EnableNoRestore());
            }
        });

    Target UpdateIcons => _ => _
        .Executes(async () => {
            Log.Information("Downloading icons meta information");
            var iconsEnumerable = await MaterialIconsMetaTools.GetIcons();
            var iconInfos = iconsEnumerable.ToList();

            var destinationPath = Solution.Directory / "Material.Icons";
            Log.Information("Writing icons meta information to {DestinationPath}", destinationPath);

            MaterialIconKindEnumGenerator.Write(destinationPath, iconInfos);
            MaterialIconDataDeclarationGenerator.Write(destinationPath);
            SwitchRegularStringGenerator.Write(destinationPath, iconInfos);
        })
        .Triggers(Compile);

    Target GhUpdateIconsWorkflow => _ => _
        .DependsOn(UpdateIcons, GhBumpVersionIfNeeded);

    Target GhBumpVersionIfNeeded => _ => _
        .OnlyWhenDynamic(() => !GitTasks.GitHasCleanWorkingCopy())
        .DependsOn(Compile)
        .Executes(() => {
            var project = Solution.GetProject("Material.Icons").GetMSBuildProject();

            var versionString = project.GetProperty("Version").EvaluatedValue;
            var newVersion = NuGetVersion.Parse(versionString).BumpLastVersion();
            project.SetProperty("Version", newVersion.ToString());

            var currentTime = DateTime.UtcNow.ToString("R", CultureInfo.InvariantCulture);
            var newReleaseNotes = $"- Icons set updated according to materialdesignicons.com at {currentTime}{Environment.NewLine}"
                                + "Check out changes at https://pictogrammers.com/library/mdi/history/";
            project.SetProperty("PackageReleaseNotes", newReleaseNotes);

            project.Save();
            Log.Information("Bumped Material.Icon property to {NewVersion}", newVersion);
        });
}
