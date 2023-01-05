using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Material.Icons;

namespace Material.Icons.Gen {
    class Program {
        static void Main(string[] args) {
            var materialIconInfos = MaterialIconsMetaTools.GetIcons().ToList();
            GenerateDataFactory(materialIconInfos);
            GenerateIconKinds(materialIconInfos);
        }

        public static void GenerateIconKinds(List<MaterialIconInfo> materialIconInfos) {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("namespace Material.Icons {");
            stringBuilder.AppendLine(
                "     /// ******************************************\n    /// This code is auto generated. Do not amend.\n    /// ******************************************");
            stringBuilder.AppendLine(
                "     /// <summary>\n    /// List of available icons.\n    /// </summary>\n    /// <remarks>\n    /// All icons sourced from Material Design Icons Font - https://materialdesignicons.com/ - in accordance of \n    /// https://github.com/Templarian/MaterialDesign/blob/master/LICENSE.\n    /// </remarks>");
            stringBuilder.AppendLine("     public enum MaterialIconKind\n    {");

            foreach (var materialIcon in materialIconInfos) {
                stringBuilder.AppendLine($"        {materialIcon.Name},");
                foreach (var iconAlias in materialIcon.Aliases) {
                    stringBuilder.AppendLine($"        {iconAlias}={materialIcon.Name},");
                }
            }

            stringBuilder.AppendLine("     }\n}");

            File.Delete("MaterialIconKind.cs");
            File.WriteAllText("MaterialIconKind.cs", stringBuilder.ToString());
        }

        public static void GenerateDataFactory(List<MaterialIconInfo> materialIconInfos) {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("using System;\nusing System.Collections.Generic;\nusing Material.Icons;");
            stringBuilder.AppendLine(
                "namespace Material.Icons\n{\n    /// ******************************************\n    /// This code is auto generated. Do not amend.\n    /// ******************************************");
            stringBuilder.AppendLine(
                "     public static class MaterialIconDataFactory\n    {\n        public static IDictionary<MaterialIconKind, string> DataSetCreate() => new Dictionary<MaterialIconKind, string> {");

            foreach (var info in materialIconInfos) {
                stringBuilder.AppendLine($"            {{MaterialIconKind.{info.Name}, \"{info.Data}\"}},");
            }

            stringBuilder.AppendLine(
                "         };\n\n");

            // Start InstanceSetCreate
            stringBuilder.AppendLine(
                "        public static IDictionary<MaterialIconKind, MaterialIconInfo> InstanceSetCreate() => new Dictionary<MaterialIconKind, MaterialIconInfo> {");
            foreach (var info in materialIconInfos) {
                stringBuilder.AppendLine($"            {{MaterialIconKind.{info.Name}, {MaterialIconsMetaTools.SerializeIcon(info)}}},");
            }

            stringBuilder.AppendLine("\n        };\n");
            // End InstanceSetCreate
            
            stringBuilder.AppendLine("    }\n}");

            File.Delete("MaterialIconDataFactory.cs");
            File.WriteAllText("MaterialIconDataFactory.cs", stringBuilder.ToString());
        }
    }
}