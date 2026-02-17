using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Lucide.Icons.Avalonia; 
/// <summary>
/// Styles for Lucide.Icons.Avalonia library
/// </summary>
public sealed class LucideIconStyles : Styles {
    /// <summary>
    /// Initializes a new instance of the <see cref="LucideIconStyles"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>
    public LucideIconStyles(IServiceProvider? serviceProvider) {
        LucideIconsUtils.InitializeGeometryParser();
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
