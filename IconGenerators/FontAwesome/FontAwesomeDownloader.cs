using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using IconGenerators.Common;

namespace IconGenerators.FontAwesome;


public  class FontAwesomeDownloader : IIconPackGenerator
{
    public string Name => "FontAwesome";
    public async Task<IEnumerable<IconInfo>> FetchIconData()
    {
        var url = "https://raw.githubusercontent.com/FortAwesome/Font-Awesome/refs/heads/master/metadata/icons.json";
        using var httpClient = new HttpClient();
        var json = await httpClient.GetStringAsync(url);

        using var doc = JsonDocument.Parse(json);
        var icons = new List<IconInfo>();

        foreach (var iconProperty in doc.RootElement.EnumerateObject())
        {
            var name = iconProperty.Name;
            if (iconProperty.Value.TryGetProperty("svg", out var svgElement))
            {
                foreach (var styleProperty in svgElement.EnumerateObject())
                {
                    if (styleProperty.Value.TryGetProperty("raw", out var rawSvgElement))
                    {
                        var rawSvg = rawSvgElement.GetString();
                        var xamlPath = ConvertSvgToXamlPath(rawSvg);
                        icons.Add(new IconInfo { Name = IconPackGenerator.ToPascalCase(name), Data = xamlPath });
                        break;
                    }
                }
            }
        }

        return icons;
    }

    private static string ConvertSvgToXamlPath(string svg)
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