using Avalonia.Media;
using Feather;

namespace Feather.Icons.Avalonia; 
internal static class FeatherIconsUtils {
    public static void InitializeGeometryParser() => FeatherIconDataProvider.InitializeGeometryParser(Geometry.Parse);
}