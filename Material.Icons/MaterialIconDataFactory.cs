using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Material.Icons {
    public static partial class MaterialIconDataFactory {
        public const string IconsMetaUrl = "https://materialdesignicons.com/api/package/38EF63D0-4744-11E4-B3CF-842B2B6CFE1B";
        /// <summary>
        /// Download actual info from materialdesignicons.com
        /// </summary>
        /// <returns>Json with data</returns>
        public static async Task<string> DownloadMetaJsonAsync() {
            var response = await WebRequest.CreateHttp(new Uri(IconsMetaUrl)).GetResponseAsync();
            using var streamReader = new StreamReader(response.GetResponseStream()!);
            return await streamReader.ReadToEndAsync();
        }

        /// <summary>
        /// Parses meta json from materialdesignicons.com
        /// </summary>
        /// <example>
        /// MaterialIconDataFactory.ParseMeta(await MaterialIconDataFactory.DownloadMetaJsonAsync());
        /// </example>
        /// <param name="metaJson">Input json</param>
        /// <returns>Collection of icons</returns>
        public static IEnumerable<MaterialIconInfo> ParseMeta(string metaJson) {
            var icons = JsonConvert.DeserializeObject<MetaMaterialIcons>(metaJson).Icons;
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
    }
}