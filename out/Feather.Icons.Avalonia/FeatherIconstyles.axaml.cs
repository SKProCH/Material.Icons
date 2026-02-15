using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Feather.Icons.Avalonia; 
/// <summary>
/// Styles for Feather.Icons.Avalonia library
/// </summary>
public sealed class FeatherIconstyles : Styles {
    /// <summary>
    /// Initializes a new instance of the <see cref="FeatherIconstyles"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>
    public FeatherIconstyles(IServiceProvider? serviceProvider) {
        FeatherIconsUtils.InitializeGeometryParser();
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
