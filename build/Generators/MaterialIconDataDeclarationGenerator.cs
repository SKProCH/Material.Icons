using System.IO;
using System.Text;
using Nuke.Common.IO;
using Serilog;

namespace Generators; 

public class MaterialIconDataDeclarationGenerator {
    public static void Write(AbsolutePath destinationPath) {
        var path = destinationPath / "MaterialIconDataProvider.Declaration.cs";
        Log.Information("Writing declaration for material icons data to {Path}", path);
        
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("using System;");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("namespace Material.Icons;");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("/// ******************************************");
        stringBuilder.AppendLine("/// This code is auto generated. Do not amend.");
        stringBuilder.AppendLine("/// ******************************************");
        stringBuilder.AppendLine("/// <summary>");
        stringBuilder.AppendLine("/// Allows retrieving data for icons");
        stringBuilder.AppendLine("/// </summary>");
        stringBuilder.AppendLine("public partial class MaterialIconDataProvider {");
        stringBuilder.AppendLine("    private static MaterialIconDataProvider? _instance;");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("    /// <summary>");
        stringBuilder.AppendLine("    /// Gets or sets the singleton instance of this provider");
        stringBuilder.AppendLine("    /// </summary>");
        stringBuilder.AppendLine("    public static MaterialIconDataProvider Instance {");
        stringBuilder.AppendLine("        get => _instance;");
        stringBuilder.AppendLine("        set {");
        stringBuilder.AppendLine("            _instance = value ?? throw new ArgumentNullException(nameof(value));");
        stringBuilder.AppendLine("        }");
        stringBuilder.AppendLine("    }");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("    /// <summary>");
        stringBuilder.AppendLine("    /// Gets the data for the specified icon using the <see cref=\"Instance\"/>");
        stringBuilder.AppendLine("    /// </summary>");
        stringBuilder.AppendLine("    /// <param name=\"kind\">The icon kind</param>");
        stringBuilder.AppendLine("    /// <returns>SVG path for target icon kind</returns>");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("    public static string GetData(MaterialIconKind kind) => Instance.ProvideData(kind);");
        stringBuilder.AppendLine("    /// <summary>");
        stringBuilder.AppendLine("    /// Provides the data for the specified icon kind");
        stringBuilder.AppendLine("    /// </summary>");
        stringBuilder.AppendLine("    /// <param name=\"kind\">The icon kind</param>");
        stringBuilder.AppendLine("    /// <returns>SVG path for target icon kind</returns>");
        stringBuilder.AppendLine("    public virtual partial string ProvideData(MaterialIconKind kind);");
        stringBuilder.AppendLine("}");
        
        File.WriteAllText(path, stringBuilder.ToString());
    }
}



























