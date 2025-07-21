
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;

namespace Material.Icons.WinUI3 {
    internal static class MaterialIconsUtils {
        public static void InitializeGeometryParser() => MaterialIconDataProvider.InitializeGeometryParser(Parser);

        private static object Parser(string source) {
            var xaml = $"""
                        <Geometry xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">{source}</Geometry>
                        """;
            return (Geometry)XamlReader.Load(xaml);
        }
    }
}