using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Material.Icons.WPF {
    public class MaterialIcon : Control {
        static MaterialIcon() {
            MaterialIconsUtils.InitializeGeometryParser();
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaterialIcon), new FrameworkPropertyMetadata(typeof(MaterialIcon)));
        }

        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(MaterialIconKind?), typeof(MaterialIcon),
                new PropertyMetadata(null, KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject,
                                                        DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
            => ((MaterialIcon) dependencyObject).UpdateData();

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public MaterialIconKind? Kind {
            get => (MaterialIconKind?) GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        public static readonly DependencyProperty AnimationProperty
            = DependencyProperty.Register(nameof(Animation), typeof(MaterialIconAnimation), typeof(MaterialIcon),
                new PropertyMetadata(default(MaterialIconAnimation)));

        /// <summary>
        /// Gets or sets the icon animation to play.
        /// </summary>
        public MaterialIconAnimation Animation {
            get => (MaterialIconAnimation)GetValue(AnimationProperty);
            set => SetValue(AnimationProperty, value);
        }

        public static readonly DependencyProperty IconSizeProperty
            = DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(MaterialIcon),
                new PropertyMetadata(double.NaN));

        /// <summary>
        /// Gets or sets the icon size
        /// </summary>
        public double IconSize {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        internal static readonly DependencyPropertyKey GeometryProperty =
            DependencyProperty.RegisterReadOnly(nameof(Geometry), typeof(Geometry), typeof(MaterialIcon),
                new PropertyMetadata(default(Geometry)));

        /// <summary>
        /// Gets the geometry for the icon
        /// </summary>
        public Geometry Geometry {
            get { return (Geometry)GetValue(GeometryProperty.DependencyProperty); }
            private set { SetValue(GeometryProperty, value); }
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            UpdateData();
        }

        private void UpdateData() {
            Geometry = MaterialIconDataProvider.Get<Geometry>(Kind) ?? Geometry.Empty;
        }
    }
}
