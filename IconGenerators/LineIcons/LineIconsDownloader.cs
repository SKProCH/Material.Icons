using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using IconGenerators.Common;

namespace IconGenerators.LineIcons
{
    public class LineIconsDownloader : IIconPackGenerator
    {
        public string Name => "LineIcon";

        public async Task<IEnumerable<IconInfo>> Fetch()
        {
            var icons = new List<IconInfo>();
            try
            {
                var url = "https://raw.githubusercontent.com/LineiconsHQ/Lineicons/main/assets/icon-fonts/unicodesMap.json";
                using var http = new HttpClient();
                var json = await http.GetStringAsync(url);

                // Parse the JSON as a dictionary
                var icons2 = JsonSerializer.Deserialize<Dictionary<string, int>>(json);

                if (icons2 == null)
                {
                    Console.WriteLine("Failed to parse icon names.");
                    return icons;
                }

                foreach (var iconName in icons2.Keys)
                {
                    // Remove "lni-" prefix if present
                    var id = iconName.StartsWith("lni-") ? iconName.Substring(4) : iconName;
                    var svgUrl = $"https://raw.githubusercontent.com/LineiconsHQ/Lineicons/refs/heads/main/assets/svgs/regular/{id}.svg";

                    try
                    {
                        var svgData = await http.GetStringAsync(svgUrl);
                        var pathData = ExtractPathData(svgData);
                        var info = new IconInfo
                        {
                            Name = id.ToPascalCase(),
                            Data = pathData,
                        };
                        if (info.Name != null)
                        icons.Add(info);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to fetch or parse SVG for {iconName}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
            }
            return icons;
        }

        private static string ExtractPathData(string svg)
        {
            if (string.IsNullOrWhiteSpace(svg))
                return string.Empty;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(svg);

                var pathNode = xmlDoc.GetElementsByTagName("path");
                if (pathNode.Count > 0 && pathNode[0].Attributes["d"] != null)
                {
                    return pathNode[0].Attributes["d"].Value;
                }
            }
            catch
            {
                // Ignore malformed SVG
            }
            return string.Empty;
        }
    }
}
