using IconGenerators.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace IconGenerators.Feather
{
    public class FeatherDownloader : IIconPackGenerator
    {
        public string Name => "Feather";
        public async Task<IEnumerable<IconInfo>> FetchIconData()
        { 
            var tagsUrl = "https://raw.githubusercontent.com/feathericons/feather/refs/heads/main/src/tags.json";
            using var http = new HttpClient();
            var json = await http.GetStringAsync(tagsUrl);

            // tags.json is a dictionary: { "iconName": [ "tag1", "tag2", ... ], ... }
            var icons = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);
            if (icons == null)
            {
                Console.WriteLine("Failed to parse icon names.");
                return null;
            }

            var iconlist = new List<IconInfo>();
            foreach (var iconName in icons.Keys)
            {
                var svgUrl = $"https://raw.githubusercontent.com/feathericons/feather/refs/heads/main/icons/{iconName}.svg";
                try
                {
                    var svgData = await http.GetStringAsync(svgUrl);
                    var pathData = ExtractPathLikeData(svgData);
                    if (string.IsNullOrEmpty(pathData))
                        continue;

                    iconlist.Add(new IconInfo { Name = IconPackGenerator.ToPascalCase(iconName), Data = pathData });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch or parse SVG for {iconName}: {ex.Message}");
                }
            }

            return iconlist;
        }
        private static string ExtractPathLikeData(string svg)
        {
            if (string.IsNullOrWhiteSpace(svg))
                return string.Empty;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(svg);

                // Try <path d="..."/>
                var pathNodes = xmlDoc.GetElementsByTagName("path");
                if (pathNodes.Count > 0 && pathNodes[0].Attributes["d"] != null)
                {
                    return pathNodes[0].Attributes["d"].Value;
                }

                // Try <polyline points="..."/>
                var polylineNodes = xmlDoc.GetElementsByTagName("polyline");
                if (polylineNodes.Count > 0 && polylineNodes[0].Attributes["points"] != null)
                {
                    var points = polylineNodes[0].Attributes["points"].Value;
                    return PolylinePointsToPathData(points, close: false);
                }

                // Try <polygon points="..."/>
                var polygonNodes = xmlDoc.GetElementsByTagName("polygon");
                if (polygonNodes.Count > 0 && polygonNodes[0].Attributes["points"] != null)
                {
                    var points = polygonNodes[0].Attributes["points"].Value;
                    return PolylinePointsToPathData(points, close: true);
                }

                // Try <circle .../>
                var circleNodes = xmlDoc.GetElementsByTagName("circle");
                if (circleNodes.Count > 0)
                {
                    var cx = circleNodes[0].Attributes["cx"]?.Value;
                    var cy = circleNodes[0].Attributes["cy"]?.Value;
                    var r = circleNodes[0].Attributes["r"]?.Value;
                    if (cx != null && cy != null && r != null && double.TryParse(cx, out var cxVal) && double.TryParse(cy, out var cyVal) && double.TryParse(r, out var rVal))
                    {
                        // XAML Path for a circle: M cx+r,cy A r,r 0 1 0 cx-r,cy A r,r 0 1 0 cx+r,cy
                        // This draws a full circle using two arcs
                        var startX = cxVal + rVal;
                        var startY = cyVal;
                        var endX = cxVal - rVal;
                        var endY = cyVal;
                        return $"M {startX},{startY} A {rVal},{rVal} 0 1 0 {endX},{endY} A {rVal},{rVal} 0 1 0 {startX},{startY}";
                    }
                }
            }
            catch
            {
                // Ignore malformed SVG
            }
            return string.Empty;
        }

        private static string PolylinePointsToPathData(string points, bool close)
        {
            // points: "x1 y1 x2 y2 x3 y3 ..."
            var matches = Regex.Matches(points, @"-?\d*\.?\d+");
            if (matches.Count < 2) return string.Empty;

            var coords = new List<string>();
            foreach (Match m in matches)
                coords.Add(m.Value);

            if (coords.Count < 4) return string.Empty;

            var sb = new System.Text.StringBuilder();
            sb.Append($"M {coords[0]},{coords[1]}");
            for (int i = 2; i < coords.Count; i += 2)
            {
                sb.Append($" L {coords[i]},{coords[i + 1]}");
            }
            if (close)
                sb.Append(" Z");
            return sb.ToString();
        }
    }
}
