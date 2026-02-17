using Avalonia.Media;
using Lucide;

namespace Lucide.Icons.Avalonia; 
internal static class LucideIconsUtils {
    public static void InitializeGeometryParser() => LucideIconDataProvider.InitializeGeometryParser(Geometry.Parse);
}