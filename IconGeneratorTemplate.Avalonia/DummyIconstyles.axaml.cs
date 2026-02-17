using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Dummy.Icons.Avalonia; 
/// <summary>
/// Styles for Dummy.Icons.Avalonia library
/// </summary>
public sealed class DummyIconStyles : Styles {
    /// <summary>
    /// Initializes a new instance of the <see cref="DummyIconStyles"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>
    public DummyIconStyles(IServiceProvider? serviceProvider) {
        DummyIconsUtils.InitializeGeometryParser();
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
