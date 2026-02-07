namespace Material.Icons.WinUI3 {
    internal static class MaterialIconsUtils {
        public static void InitializeGeometryParser() {
            MaterialIconDataProvider.DisableCache();
            MaterialIconDataProvider.InitializeGeometryParser(MaterialIconParser.Parse);
        }
    }
}