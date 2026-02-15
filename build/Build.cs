using IconGenerators;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    IEnumerable<string> GetBuildTargets()
    {
        return new List<string> {

            RootDirectory / "out" / "Feather.Icons",
            RootDirectory / "out" / "Feather.Icons.Avalonia",

            RootDirectory / "out" / "FontAwesome.Icons",
            RootDirectory / "out" / "FontAwesome.Icons.Avalonia",

            RootDirectory / "out" / "Material.Icons",
            RootDirectory / "out" / "Material.Icons.Avalonia",

            RootDirectory / "out" / "Lucide.Icons",
            RootDirectory / "out" / "Lucide.Icons.Avalonia",

            RootDirectory / "out" / "LineIcon.Icons",
            RootDirectory / "out" / "LineIcon.Icons.Avalonia",

        }.Select(x => x.ToString());
    }
}



partial class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Solution] readonly Solution Solution;

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {

            GetBuildTargets().ForEach(ExecuteRestoreFor);
            void ExecuteRestoreFor(string projectFile)
            {
                DotNetRestore(s => s
                    .SetProjectFile(projectFile));
            }
        });

    Target Restore => _ => _
        .Executes(() =>
        {
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .DependsOn(UpdateIcons)
        .Executes(() =>
        {

            GetBuildTargets().ForEach(ExecuteBuildFor);
            void ExecuteBuildFor(string projectFile)
            {
                if (projectFile.EndsWith("Avalonia"))
                {
                    DotNetPack(s => s
                        .EnableNoRestore()
                        .SetProject(projectFile)
                        //.SetProperty("OutDir", RootDirectory / "out")
                        .SetConfiguration(Configuration)
                        .SetVersion("1.2.3")
                        .SetOutputDirectory(RootDirectory / "out")
                        .EnableNoRestore()
                        .EnableIncludeSymbols()
                        .SetSymbolPackageFormat(DotNetSymbolPackageFormat.snupkg));

                }
                else
                {

                    DotNetBuild(s => s
                        .SetProjectFile(projectFile)
                        .SetConfiguration(Configuration)
                        .EnableNoRestore());
                }
            }
        });

    Target UpdateIcons => _ => _
    .Executes(async () =>
    {
       // return;
        AbsolutePath path = RootDirectory;

        List<IIconPackGenerator> generators = new List<IIconPackGenerator>();
        generators.Add(new IconGenerators.Material.MaterialDownloader());
        generators.Add(new IconGenerators.FontAwesome.FontAwesomeDownloader());
        generators.Add(new IconGenerators.LineIcons.LineIconsDownloader());
        generators.Add(new IconGenerators.Lucide.LucideDownloader());
        generators.Add(new IconGenerators.Feather.FeatherDownloader());

        var tasks = generators.Select(async generator =>
        {
            Console.WriteLine($"Gathering icons for {generator.Name}...");
            var icons = await generator.Fetch();

            Console.WriteLine($"Generating classes for {generator.Name}...");
            IconPackGenerator.Generate(icons, path, generator.Name);
        });

        await System.Threading.Tasks.Task.WhenAll(tasks);
    })
    .Triggers(Compile);


    //https://github.com/tailwindlabs/heroicons/
    //https://github.com/tabler/tabler-icons
    //https://github.com/Remix-Design/RemixIcon
}
