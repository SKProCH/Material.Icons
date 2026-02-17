using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Dummy.Icons.Avalonia;

namespace IconGenerators.Avalonia;
/// <summary>
/// Styles for Dummy.Icons.Avalonia library
/// </summary>
public sealed class UniversalIconStyles : Styles {
    /// <summary>
    /// Initializes a new instance of the <see cref="UniversalIconStyles"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>
    public UniversalIconStyles(IServiceProvider? serviceProvider) {
      //  DummyIconsUtils.InitializeGeometryParser();
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
