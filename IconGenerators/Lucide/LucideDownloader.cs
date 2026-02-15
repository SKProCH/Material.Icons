using IconGenerators.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace IconGenerators.Lucide
{
    public class LucideDownloader : IIconPackGenerator
    {
        public string Name => "Lucide";

        public async Task<IEnumerable<IconInfo>> Fetch()
        {
            var icons = new List<IconInfo>();
            var apiUrl = "https://api.github.com/repos/lucide-icons/lucide/contents/icons";
            using var http = new HttpClient();
            http.DefaultRequestHeaders.UserAgent.ParseAdd("CSharpApp"); // GitHub API requires a User-Agent

            var json = await http.GetStringAsync(apiUrl);
            var files = JsonSerializer.Deserialize<List<GitHubFile>>(json);

            if (files == null)
            {
                Console.WriteLine("Failed to get directory info from GitHub API.");
                return null;
            }

            Console.WriteLine("Lucide icon names and SVG path data:");
            foreach (var file in files)
            {
                if (file.type == "file" && file.name.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
                {
                    var iconName = file.name[..^4]; // Remove ".svg"
                    try
                    {
                        var svgData = await http.GetStringAsync(file.download_url);
                        var pathData = ExtractPathLikeData(svgData);

                        icons.Add(new IconInfo
                        {
                            Name = iconName.ToPascalCase(),
                            Data = pathData,
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to fetch or parse SVG for {iconName}: {ex.Message}");
                    }
                }
            }

            return icons;
        }


        public class GitHubFile
        {
            public string name { get; set; }
            public string download_url { get; set; }
            public string type { get; set; }
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
