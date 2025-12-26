using System.Collections.Generic;
using System.IO;
using System.Text;
using Meta;
using Nuke.Common.IO;
using Serilog;

namespace Generators.PathDataGenerators;

public class DictionaryStringGenerator {
    public static void Write(AbsolutePath destinationPath, IEnumerable<MaterialIconInfo> iconInfos) {
        var path = destinationPath / "MaterialIconDataProvider.PathData.cs";
        Log.Information("Writing enum data to {Path}", path);
        File.WriteAllText(path, GenerateIconKinds(iconInfos));
    }

    private static string GenerateIconKinds(IEnumerable<MaterialIconInfo> materialIconInfos) {
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
        stringBuilder.AppendLine("public partial class MaterialIconDataProvider {");
        stringBuilder.AppendLine("    public static IReadOnlyDictionary<MaterialIconKind, string> IconPaths { get; } = new ReadOnlyDictionary<MaterialIconKind, string>(DataSetCreate());");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("    public virtual partial string ProvideData(MaterialIconKind kind) {");
        stringBuilder.AppendLine("        return IconPaths[kind];");
        stringBuilder.AppendLine("    }");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("    [Obsolete($\"Use {nameof(IconPaths)} instead. This left only for backward compability.\")]");
        stringBuilder.AppendLine("    public static IDictionary<MaterialIconKind, string> DataSetCreate() => new Dictionary<MaterialIconKind, string> {");

        // Handle special cases
        stringBuilder.AppendLine("        {{MaterialIconKind.Invisible, string.Empty}},");
        stringBuilder.AppendLine("        {{MaterialIconKind.Transparent, string.Empty}},");

        foreach (var info in materialIconInfos) {
            stringBuilder.AppendLine($"        {{MaterialIconKind.{info.Name}, \"{info.Data}\"}},");
        }

        stringBuilder.AppendLine("    };");
        stringBuilder.AppendLine("}");

        return stringBuilder.ToString();
    }
}
