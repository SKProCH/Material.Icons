using System;
using System.Collections.Generic;

namespace Material.Icons;

/// <summary>
/// Provides access to the Material Design icons data and its geometry.
/// </summary>
public partial class MaterialIconDataProvider {
    private static MaterialIconDataProvider _instance = new();
    private static Func<string, object>? _parser;

    /// <summary>
    /// Gets the cache singleton for the icons. The cache is used to store the parsed icons to avoid parsing them multiple times.
    /// </summary>
    private static readonly Dictionary<MaterialIconKind, object> _cache = new();

    /// <summary>
    /// Gets the cache for the icons. The cache is used to store the parsed icons to avoid parsing them multiple times.
    /// </summary>
    public static IReadOnlyDictionary<MaterialIconKind, object> Cache => _cache;

    /// <summary>
    /// Gets or sets the singleton instance of this provider
    /// </summary>
    public static MaterialIconDataProvider Instance {
        get => _instance;
        set {
            _instance = value ?? throw new ArgumentNullException(nameof(value));
            ClearCache();
        }
    }

    /// <summary>
    /// Clears the cache for the icons.
    /// </summary>
    public static void ClearCache() {
        _cache.Clear();
    }

    /// <summary>
    /// Initializes the geometry parser with the specified parsing function.
    /// </summary>
    /// <remarks>This method sets the parser function to be used for geometry parsing operations. If the
    /// parser has already been initialized, subsequent calls to this method will have no effect.</remarks>
    /// <param name="parser">A function that takes a string input and returns an object representing the parsed geometry. This parameter
    /// cannot be null.</param>
    public static void InitializeGeometryParser(Func<string, object> parser) => _parser ??= parser;

    /// <summary>
    /// Gets the geometry for the specified icon using the <see cref="Instance"/>
    /// </summary>
    /// <param name="kind">The icon kind</param>
    /// <returns>SVG path for target icon kind</returns>
    public static T? Get<T>(MaterialIconKind? kind) where T : class {
        if (!kind.HasValue) return null;

        if (Cache.TryGetValue(kind.Value, out var value)) {
            return value as T ?? throw new InvalidOperationException(
                "Invalid type for icon kind. Check that you are requesting the correct geometry type.");
        }

        if (_parser is null) {
            throw new InvalidOperationException(
                "Geometry parser not initialized. Call InitializeGeometryParser first.");
        }

        var result = _parser(GetData(kind.Value)) as T ?? throw new InvalidOperationException(
            "Parser returns a wrong type. Check that you are requesting the correct geometry type.");

        _cache[kind.Value] = result;

        return result;
    }

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