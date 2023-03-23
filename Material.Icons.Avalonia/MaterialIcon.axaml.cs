using Avalonia;
using Avalonia.Controls.Primitives;

namespace Material.Icons.Avalonia {
    public class MaterialIcon : TemplatedControl {
        public static readonly StyledProperty<MaterialIconKind> KindProperty
            = AvaloniaProperty.Register<MaterialIcon, MaterialIconKind>(nameof(Kind));

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public MaterialIconKind Kind {
            get => GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }
    }
}
