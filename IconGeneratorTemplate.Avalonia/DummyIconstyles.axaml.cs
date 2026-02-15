using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Dummy.Icons.Avalonia; 
/// <summary>
/// Styles for Dummy.Icons.Avalonia library
/// </summary>
public sealed class DummyIconstyles : Styles {
    /// <summary>
    /// Initializes a new instance of the <see cref="DummyIconstyles"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>
    public DummyIconstyles(IServiceProvider? serviceProvider) {
        DummyIconsUtils.InitializeGeometryParser();
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
