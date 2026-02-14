using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Meta;
using Nuke.Common.IO;

namespace Generators;


public static class FontAwesomeIconGenerator {
    public static void Generate(IEnumerable<IconInfo> icons, AbsolutePath dir) {

        var enumOutputFile = dir / "FontAwesomeEnumEnum.cs";
        var pathDataOutputFile = dir / "FontAwesomeIconDataProvider.PathData.cs";

        // Generate enum file
        var enumSb = new StringBuilder();
        enumSb.AppendLine("namespace FontAwesome.Icons;");
        enumSb.AppendLine("/// ******************************************");
        enumSb.AppendLine("/// This code is auto generated. Do not amend.");
        enumSb.AppendLine("/// ******************************************");
        enumSb.AppendLine("/// <summary>");
        enumSb.AppendLine("/// List of available icon kinds");
        enumSb.AppendLine("/// </summary>");
        enumSb.AppendLine("public enum FontAwesomeIconKind");
        enumSb.AppendLine("{");
        foreach (var icon in icons)
            enumSb.AppendLine($"    {icon.Name},");
        enumSb.AppendLine("}");
        File.WriteAllText(enumOutputFile, enumSb.ToString());

        // Generate path data provider file
        var pathSb = new StringBuilder();
        pathSb.AppendLine("using System.Runtime.CompilerServices;");
        pathSb.AppendLine();
        pathSb.AppendLine("namespace FontAwesome.Icons;");
        pathSb.AppendLine("/// ******************************************");
        pathSb.AppendLine("/// This code is auto generated. Do not amend.");
        pathSb.AppendLine("/// ******************************************");
        pathSb.AppendLine("public partial class FontAwesomeIconDataProvider");
        pathSb.AppendLine("{");
        pathSb.AppendLine("    [MethodImpl(MethodImplOptions.AggressiveInlining)]");
        pathSb.AppendLine("    public virtual partial string ProvideData(FontAwesomeIconKind kind)");
        pathSb.AppendLine("        => kind switch");
        pathSb.AppendLine("        {");
        foreach (var icon in icons) {
            var escapedXaml = icon.Data.Replace("\"", "\"\"");
            pathSb.AppendLine($"            FontAwesomeIconKind.{icon.Name} => @\"{escapedXaml}\",");
        }
        pathSb.AppendLine("            _ => string.Empty");
        pathSb.AppendLine("        };");
        pathSb.AppendLine("}");
        File.WriteAllText(pathDataOutputFile, pathSb.ToString());
    }

    public static string ToPascalCase(string name) {
        var parts = Regex.Split(name, @"[^a-zA-Z0-9]+");
        var pascal = new StringBuilder();
        foreach (var part in parts) {
            if (string.IsNullOrEmpty(part))
                continue;
            pascal.Append(char.ToUpperInvariant(part[0]));
            if (part.Length > 1)
                pascal.Append(part.Substring(1));
        }
        var result = pascal.ToString();
        if (!string.IsNullOrEmpty(result) && char.IsDigit(result[0]))
            result = "_" + result;
        return result;
    }
}
