using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Material.Icons {
    public static class MaterialIconsMetaTools {
        public static IEnumerable<MaterialIcon> GetIcons() {
            using var webClient = new WebClient();
            var data = webClient.DownloadString("https://materialdesignicons.com/api/package/38EF63D0-4744-11E4-B3CF-842B2B6CFE1B");
            var icons = JsonConvert.DeserializeObject<MetaMaterialIcons>(data).Icons;
            var iconsByName = new Dictionary<string, MaterialIcon>(StringComparer.OrdinalIgnoreCase);
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

        public static string SerializeIcon(MaterialIcon icon) {
            var builder = new StringBuilder("new MaterialIcon { ");

            builder.Append($"Name = \"{icon.Name}\", ");
            builder.Append($"Id = \"{icon.Id}\", ");
            builder.Append($"Data = \"{icon.Data}\", ");

            builder.Append("Aliases = new List<string> { ");
            foreach (var iconAlias in icon.Aliases) {
                builder.Append($"\"{iconAlias}\", ");
            }

            // if (icon.Aliases.Count != 0) {
            //     builder.Remove(builder.Length - 2, 2);
            // }
            
            builder.Append(" }, ");

            builder.Append($"User = new MaterialIconUser {{ Id = \"{icon.User.Id}\", Name = \"{icon.User.Name}\" }}");

            builder.Append(" }");
            // builder.Replace("\"", "\\\"");
            return builder.ToString();
        }
    }
}