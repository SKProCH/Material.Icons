using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Material.Icons.Avalonia; 
/// <summary>
/// Styles for Material.Icons.Avalonia library
/// </summary>
public sealed class MaterialIconstyles : Styles {
    /// <summary>
    /// Initializes a new instance of the <see cref="MaterialIconstyles"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>
    public MaterialIconstyles(IServiceProvider? serviceProvider) {
        MaterialIconsUtils.InitializeGeometryParser();
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
