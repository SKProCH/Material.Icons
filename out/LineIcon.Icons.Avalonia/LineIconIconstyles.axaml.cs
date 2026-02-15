using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace LineIcon.Icons.Avalonia; 
/// <summary>
/// Styles for LineIcon.Icons.Avalonia library
/// </summary>
public sealed class LineIconIconstyles : Styles {
    /// <summary>
    /// Initializes a new instance of the <see cref="LineIconIconstyles"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>
    public LineIconIconstyles(IServiceProvider? serviceProvider) {
        LineIconIconsUtils.InitializeGeometryParser();
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
