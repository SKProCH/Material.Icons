using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace Material.Icons.Avalonia {
    public class MaterialIconText : MaterialIcon {
        public static readonly StyledProperty<double> SpacingProperty =
            StackPanel.SpacingProperty.AddOwner<MaterialIconText>();

        public static readonly StyledProperty<Orientation> OrientationProperty =
            StackPanel.OrientationProperty.AddOwner<MaterialIconText>();

        public static readonly StyledProperty<string?> TextProperty =
            TextBlock.TextProperty.AddOwner<MaterialIconText>();

        public static readonly StyledProperty<bool> TextFirstProperty =
            AvaloniaProperty.Register<MaterialIconText, bool>(nameof(TextFirst));

        public static readonly StyledProperty<bool> IsTextSelectableProperty =
            AvaloniaProperty.Register<MaterialIconText, bool>(nameof(IsTextSelectable));

        public static readonly StyledProperty<double> IconSizeProperty =
            AvaloniaProperty.Register<MaterialIconText, double>(nameof(IconSize), defaultValue: double.NaN);

        public double Spacing {
            get => GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        public Orientation Orientation {
            get => GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public string? Text {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool TextFirst {
            get => GetValue(TextFirstProperty);
            set => SetValue(TextFirstProperty, value);
        }

        public bool IsTextSelectable {
            get => GetValue(IsTextSelectableProperty);
            set => SetValue(IsTextSelectableProperty, value);
        }

        public double IconSize {
            get => GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }
    }
}
