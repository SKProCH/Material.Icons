using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Material.Icons.Avalonia {

    public class MaterialIcon : TemplatedControl, IImage {
        #region Properties

        public static readonly StyledProperty<MaterialIconKind?> KindProperty
            = AvaloniaProperty.Register<MaterialIcon, MaterialIconKind?>(nameof(Kind));

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public MaterialIconKind? Kind {
            get => GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        public static readonly StyledProperty<double> IconSizeProperty =
            AvaloniaProperty.Register<MaterialIconText, double>(nameof(IconSize), defaultValue: double.NaN);

        /// <summary>
        /// Gets or sets the uniform size of the icon.
        /// </summary>
        public double IconSize {
            get => GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        public static readonly StyledProperty<MaterialIconAnimation> AnimationProperty
            = AvaloniaProperty.Register<MaterialIcon, MaterialIconAnimation>(nameof(Animation));

        /// <summary>
        /// Gets or sets the icon animation to play.
        /// </summary>
        public MaterialIconAnimation Animation {
            get => GetValue(AnimationProperty);
            set => SetValue(AnimationProperty, value);
        }

        public static readonly DirectProperty<MaterialIcon, GeometryDrawing> DrawingProperty =
            AvaloniaProperty.RegisterDirect<MaterialIcon, GeometryDrawing>(
                nameof(Drawing),
                o => o.Drawing);

        /// <summary>
        /// Gets the <see cref="GeometryDrawing"/> of the icon.
        /// </summary>
        public GeometryDrawing Drawing { get; } = new();

        // Default size for Material Icons
        private static readonly Rect DefaultIconBounds = new(0, 0, 24, 24);

        #endregion

        #region Constructor

        static MaterialIcon() {
            MaterialIconsUtils.InitializeGeometryParser();
        }

        public MaterialIcon() {
            Drawing.Brush = Foreground;
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        protected override void OnLoaded(RoutedEventArgs e) {
            if (Drawing.Geometry is null)
                SetGeometry();
            base.OnLoaded(e);
        }

        /// <inheritdoc />
        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e) {
            base.OnPropertyChanged(e);

            if (e.Property == KindProperty) {
                SetGeometry();
            }
            else if (e.Property == ForegroundProperty) {
                Drawing.Brush = Foreground;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the geometry for the drawing based on the specified material icon kind.
        /// </summary>
        /// <remarks>This method updates the <see cref="Drawing"/> Geometry property by parsing the
        /// geometry data associated with the current <see cref="Kind"/> value.
        /// </remarks>
        private void SetGeometry() {
            Drawing.Geometry = Kind is null ? null : MaterialIconDataProvider.Get<Geometry>(Kind.Value);
        }

        #endregion

        #region IImage Implementation

        /// <inheritdoc/>
        Size IImage.Size => DefaultIconBounds.Size;

        /// <inheritdoc/>
        void IImage.Draw(DrawingContext context, Rect sourceRect, Rect destRect) {
            if (Drawing.Geometry is null)
                SetGeometry();

            var bounds = DefaultIconBounds;
            var scale = Matrix.CreateScale(
                destRect.Width / sourceRect.Width,
                destRect.Height / sourceRect.Height
            );
            var translate = Matrix.CreateTranslation(
                -sourceRect.X + destRect.X - bounds.X,
                -sourceRect.Y + destRect.Y - bounds.Y
            );

            using (context.PushClip(destRect))
            using (context.PushTransform(translate * scale)) {
                Drawing.Draw(context);
            }
        }

        #endregion

    }
}
