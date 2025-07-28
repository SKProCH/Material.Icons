using Avalonia.Media;

namespace Material.Icons.Avalonia {
    internal static class MaterialIconsUtils {
        public static void InitializeGeometryParser() => MaterialIconDataProvider.InitializeGeometryParser(Geometry.Parse);
    }
}