using Avalonia.Media;
using IconGenerators;

namespace Dummy.Icons.Avalonia; 
internal static class DummyIconsUtils {
    public static void InitializeGeometryParser() => DummyIconDataProvider.InitializeGeometryParser(Geometry.Parse);
}