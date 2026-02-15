using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IconGenerators.Common;
using Nuke.Common.IO;

namespace IconGenerators;

public static class IconPackGenerator
{
    public static void Generate(IEnumerable<IconInfo> icons, AbsolutePath dir, string iconPackName)
    {
        var targetDir = dir / "out" / iconPackName + ".Icons";

        // Copy and adapt IconGeneratorTemplate.Avalonia project
        var templateDir = dir / "IconGeneratorTemplate.Avalonia";
        var targetProjectDir = dir / "out" /  (iconPackName + ".Icons.Avalonia");
        if (!Directory.Exists(targetProjectDir))
            Directory.CreateDirectory(targetProjectDir);

        foreach (var file in Directory.GetFiles(templateDir, "*", SearchOption.TopDirectoryOnly))
        {
            var fileName = Path.GetFileName(file);
            if (fileName == "obj" || fileName.EndsWith(".user")) continue;

            var fileContent = File.ReadAllText(file);
            // Replace Dummy, namespace, and using IconGenerators;
            fileContent = fileContent.Replace("Dummy", iconPackName);
            fileContent = Regex.Replace(fileContent, @"namespace\\s+Dummy\\.Icons\\.Avalonia", $"namespace {iconPackName}.Icons.Avalonia");
            fileContent = Regex.Replace(fileContent, @"using\s+IconGenerators;", $"using {iconPackName};");

            // If this is the .csproj file, update the project reference
            if (fileName.EndsWith(".csproj"))
            {
                fileContent = Regex.Replace(
                    fileContent,
                    @"<ProjectReference Include=""\.\.\\IconGenerators\\IconGenerators\.csproj""\s*/>",
                    $"<ProjectReference Include=\"..\\{iconPackName}.Icons\\{iconPackName}.Icons.csproj\" />");
              //  /*
                // Replace the <None Include="..\\Dummy.Icons\\bin\\$(Configuration)\\net10.0\\Dummy.Icons.dll" ... /> line
            //    fileContent = Regex.Replace(
            //        fileContent,
            //        @"<None\s+Include=""\.\.\\\\Dummy\.Icons\\\\bin\\\\\$\(Configuration\)\\\\net10\.0\\\\Dummy\.Icons\.dll""\s+Pack=""true""\s+PackagePath=""lib\\\\net10\.0\\\\""(\s*/>|>)",
                 //   $"<None Include=\"..\\{iconPackName}.Icons\\bin\\$(Configuration)\\net10.0\\{iconPackName}.Icons.dll\" Pack=\"true\" PackagePath=\"lib\\net10.0\\\"$1");
               // */
                // Ensure all remaining 'Dummy' are replaced
                fileContent = fileContent.Replace("Dummy", iconPackName);
            }

            var newFileName = fileName.Replace("Dummy", iconPackName).Replace("IconGeneratorTemplate.Avalonia", iconPackName + ".Icons.Avalonia");
            var newFilePath = Path.Combine(targetProjectDir, newFileName);
            File.WriteAllText(newFilePath, fileContent);
        }
    
        var enumTypeName = $"{iconPackName}IconKind";
        var dataProviderTypeName = $"{iconPackName}IconDataProvider";
       
        if (!Directory.Exists(targetDir))
            Directory.CreateDirectory(targetDir);
        // Copy and adapt DummyIconDataProvider
        var assembly = typeof(IconPackGenerator).Assembly;
        var resourceName = assembly.GetManifestResourceNames()
            .FirstOrDefault(n => n.EndsWith("DummyIconDataProvider.cs"));
        if (resourceName != null)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            var dummyProviderContent = reader.ReadToEnd();
            var replacedContent = dummyProviderContent.Replace("Dummy", iconPackName);
            // Replace the namespace line with the correct namespace
            replacedContent = System.Text.RegularExpressions.Regex.Replace(
                replacedContent,
                @"^namespace\s+.*$",
                $"namespace {iconPackName}.Icons;",
                System.Text.RegularExpressions.RegexOptions.Multiline);
            var outputProviderPath = targetDir / $"{iconPackName}IconDataProvider.cs";
            File.WriteAllText(outputProviderPath, replacedContent);
        }
        var enumOutputFile = targetDir / $"{enumTypeName}.cs";
        var pathDataOutputFile = targetDir / $"{dataProviderTypeName}.PathData.cs";
        var csprojFile = targetDir / $"{iconPackName}.Icons.csproj";

        // Generate enum file
        var enumSb = new StringBuilder();
        enumSb.AppendLine($"namespace {iconPackName}.Icons;");
        enumSb.AppendLine("/// ******************************************");
        enumSb.AppendLine("/// This code is auto generated. Do not amend.");
        enumSb.AppendLine("/// ******************************************");
        enumSb.AppendLine("/// <summary>");
        enumSb.AppendLine("/// List of available icon kinds");
        enumSb.AppendLine("/// </summary>");
        enumSb.AppendLine($"public enum {enumTypeName}");
        enumSb.AppendLine("{");
        foreach (var icon in icons)
            enumSb.AppendLine($"    {icon.Name},");
        enumSb.AppendLine("}");
        File.WriteAllText(enumOutputFile, enumSb.ToString());

        // Generate path data provider file
        var pathSb = new StringBuilder();
        pathSb.AppendLine("using System.Runtime.CompilerServices;");
        pathSb.AppendLine();
        pathSb.AppendLine($"namespace {iconPackName}.Icons;");
        pathSb.AppendLine("/// ******************************************");
        pathSb.AppendLine("/// This code is auto generated. Do not amend.");
        pathSb.AppendLine("/// ******************************************");
        pathSb.AppendLine($"public partial class {dataProviderTypeName}");
        pathSb.AppendLine("{");
        pathSb.AppendLine("    [MethodImpl(MethodImplOptions.AggressiveInlining)]");
        pathSb.AppendLine($"    public virtual partial string ProvideData({enumTypeName} kind)");
        pathSb.AppendLine("        => kind switch");
        pathSb.AppendLine("        {");
        foreach (var icon in icons)
        {
            var escapedData = icon.Data.Replace("\"", "\"\"");
            pathSb.AppendLine($"            {enumTypeName}.{icon.Name} => @\"{escapedData}\",");
        }
        pathSb.AppendLine("            _ => string.Empty");
        pathSb.AppendLine("        };");
        pathSb.AppendLine("}");
        File.WriteAllText(pathDataOutputFile, pathSb.ToString());

        // Generate .csproj file
        var csprojSb = new StringBuilder();
        csprojSb.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
        csprojSb.AppendLine("  <PropertyGroup>");
        csprojSb.AppendLine("    <TargetFramework>net10.0</TargetFramework>");
        csprojSb.AppendLine("    <Nullable>enable</Nullable>");
        csprojSb.AppendLine("    <LangVersion>latest</LangVersion>");
        csprojSb.AppendLine("  </PropertyGroup>");
        csprojSb.AppendLine("</Project>");
        File.WriteAllText(csprojFile, csprojSb.ToString());
    }

    public static string ToPascalCase(this string name)
    {
        // Split on any non-alphanumeric character (including hyphens) to remove them
        var parts = Regex.Split(name, @"[^a-zA-Z0-9]+");
        var pascal = new StringBuilder();
        foreach (var part in parts)
        {
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