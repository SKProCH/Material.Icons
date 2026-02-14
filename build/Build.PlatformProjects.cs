using System;
using System.Collections.Generic;
using Serilog;
partial class Build {
    IEnumerable<string> GetBuildTargets() {
        var includeWpf = Environment.OSVersion.Platform == PlatformID.Win32NT && !SkipWpfCompilation;
        if (!includeWpf) {
            Log.Information("Due to running on non Windows or SkipWpfCompilation=True, we will skip WPF projects");
            return new List<string> {
                "FontAwesome.Icons",
                "Material.Icons",
                "Material.Icons.Avalonia",
                "Material.Icons.Avalonia.Demo"
            };
        }
        return new List<string> { Solution };
    }
}
