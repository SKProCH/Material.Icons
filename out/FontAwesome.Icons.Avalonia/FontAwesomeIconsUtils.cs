using Avalonia.Media;
using FontAwesome;

namespace FontAwesome.Icons.Avalonia; 
internal static class FontAwesomeIconsUtils {
    public static void InitializeGeometryParser() => FontAwesomeIconDataProvider.InitializeGeometryParser(Geometry.Parse);
}