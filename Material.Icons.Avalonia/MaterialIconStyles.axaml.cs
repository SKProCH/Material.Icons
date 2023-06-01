using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Material.Icons.Avalonia {
    /// <summary>
    /// Styles for Material.Icons.Avalonia library
    /// </summary>
    public sealed class MaterialIconStyles : Styles {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialThemeBase"/> class.
        /// </summary>
        /// <param name="serviceProvider">The parent's service provider.</param>
        public MaterialIconStyles(IServiceProvider? serviceProvider) {
            AvaloniaXamlLoader.Load(serviceProvider, this);
        }
    }
}
