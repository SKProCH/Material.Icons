using Avalonia.Media;
using LineIcon;

namespace LineIcon.Icons.Avalonia; 
internal static class LineIconIconsUtils {
    public static void InitializeGeometryParser() => LineIconIconDataProvider.InitializeGeometryParser(Geometry.Parse);
}