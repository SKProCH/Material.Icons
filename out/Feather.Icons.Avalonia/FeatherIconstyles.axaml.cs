using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Feather.Icons.Avalonia; 
/// <summary>
/// Styles for Feather.Icons.Avalonia library
/// </summary>
public sealed class FeatherIconStyles : Styles {
    /// <summary>
    /// Initializes a new instance of the <see cref="FeatherIconStyles"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>
    public FeatherIconStyles(IServiceProvider? serviceProvider) {
        FeatherIconsUtils.InitializeGeometryParser();
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
