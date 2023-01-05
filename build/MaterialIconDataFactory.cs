using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Material.Icons;
public static class MaterialIconsMetaTools {
    static readonly HttpClient HttpClient = new();
    const string MaterialIconsFetchApi = "https://materialdesignicons.com/api/package/38EF63D0-4744-11E4-B3CF-842B2B6CFE1B";

    public static async Task<IEnumerable<MaterialIconInfo>> GetIcons() {
        var dataStream = await HttpClient.GetStreamAsync(MaterialIconsFetchApi);
        var dataNode = await JsonSerializer.DeserializeAsync<MetaMaterialIcons>(dataStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var icons = dataNode.Icons;
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

    internal class MetaMaterialIcons {
        public List<MaterialIconInfo> Icons { get; set; }
    }
}