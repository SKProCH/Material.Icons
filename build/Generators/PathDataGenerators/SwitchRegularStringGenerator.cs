using System.Collections.Generic;
using System.IO;
using System.Text;
using Meta;
using Nuke.Common.IO;
using Serilog;

namespace Generators.PathDataGenerators;

public class SwitchRegularStringGenerator {
    public static void Write(AbsolutePath destinationPath, IEnumerable<IconInfo> iconInfos) {
        var path = destinationPath / "MaterialIconDataProvider.PathData.cs";
        Log.Information("Writing enum data to {Path}", path);
        File.WriteAllText(path, GenerateIconKinds(iconInfos));
    }

    private static string GenerateIconKinds(IEnumerable<IconInfo> materialIconInfos) {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("using System;");
        stringBuilder.AppendLine("using System.Collections.Generic;");
        stringBuilder.AppendLine("using System.Collections.ObjectModel;");
        stringBuilder.AppendLine("using System.Runtime.CompilerServices;");
        stringBuilder.AppendLine("using Material.Icons;");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("namespace Material.Icons;");
        stringBuilder.AppendLine("/// ******************************************");
        stringBuilder.AppendLine("/// This code is auto generated. Do not amend.");
        stringBuilder.AppendLine("/// ******************************************");
        stringBuilder.AppendLine("public partial class MaterialIconDataProvider {");
        stringBuilder.AppendLine("    [MethodImpl(MethodImplOptions.AggressiveInlining)]");
        stringBuilder.AppendLine("    public virtual partial string ProvideData(MaterialIconKind kind) {");
        stringBuilder.AppendLine("        return kind switch {");

        // Handle special cases
        foreach (var token in MaterialIconToken.Tokens) {
            stringBuilder.AppendLine($"        {token.DataPathSwitchDefinition}");
        }

        foreach (var materialIcon in materialIconInfos) {
            stringBuilder.AppendLine($"        {GetSwitchLine(materialIcon)},");
        }

        stringBuilder.AppendLine("        };");
        stringBuilder.AppendLine("    }");
        stringBuilder.AppendLine("}");

        return stringBuilder.ToString();
    }
    static string GetSwitchLine(IconInfo materialIcon) =>
        $"MaterialIconKind.{materialIcon.Name} => \"{materialIcon.Data}\"";
}
