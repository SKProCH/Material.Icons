using System.Collections.Generic;
using System;

namespace Material.Icons;

/// <summary>
/// Global options for the Material Icons library.
/// </summary>
public static class MaterialIconOptions {
    /// <summary>
    /// Gets or sets a value indicating whether to use the cache for the icon, if true the icon won't be parsed twice.
    /// </summary>
    public static bool UseCache { get; set; }

    /// <summary>
    /// Gets the cache singleton for the icons. The cache is used to store the parsed icons to avoid parsing them multiple times.
    /// </summary>

    private static readonly Lazy<Dictionary<MaterialIconKind, object>> CacheLazy = new(() => new Dictionary<MaterialIconKind, object>());

    /// <summary>
    /// Gets the cache for the icons. The cache is used to store the parsed icons to avoid parsing them multiple times.
    /// </summary>
    public static Dictionary<MaterialIconKind, object> Cache => CacheLazy.Value;

    /// <summary>
    /// Clears the cache for the icons.
    /// </summary>
    public static void ClearCache() {
        if (CacheLazy.IsValueCreated) Cache.Clear();
    }
}
