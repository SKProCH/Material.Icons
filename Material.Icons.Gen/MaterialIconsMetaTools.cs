using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Material.Icons;
using Newtonsoft.Json;

namespace Material.Icons.Gen {
    public static class MaterialIconsMetaTools {
        public static IEnumerable<MaterialIconInfo> GetIcons() {
            using var webClient = new WebClient();
            var data = webClient.DownloadString("https://materialdesignicons.com/api/package/38EF63D0-4744-11E4-B3CF-842B2B6CFE1B");
            var icons = JsonConvert.DeserializeObject<MetaMaterialIcons>(data).Icons;
            var iconsByName = new Dictionary<string, MaterialIconInfo>(StringComparer.OrdinalIgnoreCase);
            foreach (var icon in icons.Where(icon => !iconsByName.ContainsKey(icon.Name))) {
                iconsByName.Add(icon.Name, icon);
            }

            //Clean up aliases to avoid naming collisions
            var seenAliases = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var icon in iconsByName.Values) {
                seenAliases.Add(icon.Name);
            }

            foreach (var icon in iconsByName.Values) {
                for (var i = icon.Aliases.Count - 1; i >= 0; i--) {
                    var alias = icon.Aliases[i];
                    if (iconsByName.ContainsKey(alias) || !IsValidIdentifier(alias) || seenAliases.Add(alias) == false) {
                        icon.Aliases.RemoveAt(i);
                    }
                }
            }

            return iconsByName.Values.OrderBy(x => x.Name);

            static bool IsValidIdentifier(string identifier) {
                return identifier?.Length > 0 && (char.IsLetter(identifier[0]) || identifier[0] == '_');
            }
        }

        public static string SerializeIcon(MaterialIconInfo iconInfo) {
            var builder = new StringBuilder("new MaterialIconInfo { ");

            builder.Append($"Name = \"{iconInfo.Name}\", ");
            builder.Append($"Id = \"{iconInfo.Id}\", ");
            builder.Append($"Data = \"{iconInfo.Data}\", ");

            builder.Append("Aliases = new List<string> { ");
            foreach (var iconAlias in iconInfo.Aliases) {
                builder.Append($"\"{iconAlias}\", ");
            }

            builder.Append(" }, ");

            builder.Append($"User = new MaterialIconUser {{ Id = \"{iconInfo.User.Id}\", Name = \"{iconInfo.User.Name}\" }}");

            builder.Append(" }");
            return builder.ToString();
        }
    }
}