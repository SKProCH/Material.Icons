using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Meta;
using Nuke.Common.IO;
using Serilog;

namespace Generators; 

public static class MaterialIconKindEnumGenerator {
    public static void Write(AbsolutePath destinationPath, IEnumerable<IconInfo> iconInfos) {
        var path = destinationPath / "MaterialIconEnum.cs";
        Log.Information("Writing kinds enum to {Path}", path);
        File.WriteAllText(path, GenerateIconKinds(iconInfos));
    }
    
    private static string GenerateIconKinds(IEnumerable<IconInfo> materialIconInfos) {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("namespace Material.Icons;");
        stringBuilder.AppendLine("/// ******************************************");
        stringBuilder.AppendLine("/// This code is auto generated. Do not amend.");
        stringBuilder.AppendLine("/// ******************************************");
        stringBuilder.AppendLine("/// <summary>");
        stringBuilder.AppendLine("/// List of available icon kinds");
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
}
