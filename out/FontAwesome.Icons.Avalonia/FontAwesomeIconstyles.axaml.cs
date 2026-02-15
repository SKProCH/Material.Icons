using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace FontAwesome.Icons.Avalonia; 
/// <summary>
/// Styles for FontAwesome.Icons.Avalonia library
/// </summary>
public sealed class FontAwesomeIconstyles : Styles {
    /// <summary>
    /// Initializes a new instance of the <see cref="FontAwesomeIconstyles"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>
    public FontAwesomeIconstyles(IServiceProvider? serviceProvider) {
        FontAwesomeIconsUtils.InitializeGeometryParser();
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
