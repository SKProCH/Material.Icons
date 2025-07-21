using System.Windows.Media;

namespace Material.Icons.WPF {
    internal static class MaterialIconsUtils {
        public static void InitializeGeometryParser() =>
            MaterialIconDataProvider.InitializeGeometryParser(Geometry.Parse);
    }
}