using System.Windows;
using System.Windows.Controls;

namespace Material.Icons.WPF {
    public class MaterialIconText : MaterialIcon {
        static MaterialIconText() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaterialIconText), new FrameworkPropertyMetadata(typeof(MaterialIconText)));
        }

        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
            nameof(Spacing), typeof(double), typeof(MaterialIconText), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty OrientationProperty =
            StackPanel.OrientationProperty.AddOwner(typeof(MaterialIconText));

        public static readonly DependencyProperty TextProperty =
                TextBlock.TextProperty.AddOwner(typeof(MaterialIconText));

        public static readonly DependencyProperty TextFirstProperty = DependencyProperty.Register(
            nameof(TextFirst), typeof(bool), typeof(MaterialIconText), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register(
            nameof(IconSize), typeof(double), typeof(MaterialIconText), new PropertyMetadata(double.NaN));

        public double Spacing {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        public Orientation Orientation {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public string? Text {
            get => (string?)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool TextFirst {
            get => (bool)GetValue(TextFirstProperty);
            set => SetValue(TextFirstProperty, value);
        }

        public double IconSize {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }
    }
}
