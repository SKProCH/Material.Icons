namespace Material.Icons;

/// ******************************************
/// This code is auto generated. Do not amend.
/// ******************************************
/// <summary>
/// Allows retrieving data for icons
/// </summary>
public partial class MaterialIconDataProvider {
    private static MaterialIconDataProvider? _instance;
    /// <summary>
    /// Gets the singleton instance of this provider
    /// </summary>
    public static MaterialIconDataProvider Instance => _instance ??= new MaterialIconDataProvider();
    /// <summary>
    /// Gets the data for the specified icon using the <see cref="Instance"/>
    /// </summary>
    /// <param name="kind">The icon kind</param>
    /// <returns>SVG path for target icon kind</returns>
    public static string GetData(MaterialIconKind kind) => Instance.ProvideData(kind);
    /// <summary>
    /// Provides the data for the specified icon kind
    /// </summary>
    /// <param name="kind">The icon kind</param>
    /// <returns>SVG path for target icon kind</returns>
    public virtual partial string ProvideData(MaterialIconKind kind);
}
