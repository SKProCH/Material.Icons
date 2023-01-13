using Avalonia;
using Avalonia.Controls.Primitives;

namespace Material.Icons.Avalonia {
    public class MaterialIcon : TemplatedControl {
        public static readonly AvaloniaProperty<MaterialIconKind> KindProperty
            = AvaloniaProperty.RegisterDirect<MaterialIcon, MaterialIconKind>(nameof(Kind),
                icon => icon.Kind,
                (icon, kind) => icon.Kind = kind);

        private MaterialIconKind _kind;

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public MaterialIconKind Kind {
            get => _kind;
            set => SetAndRaise(KindProperty, ref _kind, value);
        }
    }
}