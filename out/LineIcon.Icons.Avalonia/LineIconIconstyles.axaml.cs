using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace LineIcon.Icons.Avalonia; 
/// <summary>
/// Styles for LineIcon.Icons.Avalonia library
/// </summary>
public sealed class LineIconIconStyles : Styles {
    /// <summary>
    /// Initializes a new instance of the <see cref="LineIconIconStyles"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>
    public LineIconIconStyles(IServiceProvider? serviceProvider) {
        LineIconIconsUtils.InitializeGeometryParser();
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
