using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace IconGenerators.Avalonia;

public class UniversalIcon : TemplatedControl, IImage
{
    #region Properties

    public static readonly StyledProperty<IIconProvider?> ProviderProperty =
        AvaloniaProperty.Register<UniversalIcon, IIconProvider?>(nameof(Provider));

    public IIconProvider? Provider
    {
        get => GetValue(ProviderProperty);
        set => SetValue(ProviderProperty, value);
    }

    public static readonly StyledProperty<string?> IconProperty =
        AvaloniaProperty.Register<UniversalIcon, string?>(nameof(Icon));

    public string? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<UniversalIcon, double>(nameof(IconSize), defaultValue: double.NaN);

    public double IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    public static readonly StyledProperty<IconAnimation> AnimationProperty
    = AvaloniaProperty.Register<UniversalIcon, IconAnimation>(nameof(Animation));

    /// <summary>
    /// Gets or sets the icon animation to play.
    /// </summary>
    public IconAnimation Animation
    {
        get => GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }

    public static readonly DirectProperty<UniversalIcon, GeometryDrawing> DrawingProperty =
        AvaloniaProperty.RegisterDirect<UniversalIcon, GeometryDrawing>(
            nameof(Drawing),
            o => o.Drawing);

    public GeometryDrawing Drawing { get; } = new();

    private static readonly Rect DefaultIconBounds = new(0, 0, 24, 24);

    #endregion

    #region Constructor

    public UniversalIcon()
    {
        Drawing.Brush = Foreground;
    }

    #endregion

    #region Overrides

    protected override void OnLoaded(RoutedEventArgs e)
    {
        if (Drawing.Geometry is null)
            SetGeometry();
        base.OnLoaded(e);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.Property == ProviderProperty || e.Property == IconProperty)
        {
            SetGeometry();
        }
        else if (e.Property == ForegroundProperty)
        {
            Drawing.Brush = Foreground;
        }
    }

    #endregion

    #region Methods

    private void SetGeometry()
    {
        if (Provider is not null && !string.IsNullOrEmpty(Icon))
        {
            var data = Provider.ProvideData(Icon);
            Drawing.Geometry = !string.IsNullOrEmpty(data) ? Geometry.Parse(data) : null;
        }
        else
        {
            Drawing.Geometry = null;
        }
    }

    #endregion

    #region IImage Implementation

    Size IImage.Size => DefaultIconBounds.Size;

    void IImage.Draw(DrawingContext context, Rect sourceRect, Rect destRect)
    {
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
        using (context.PushTransform(translate * scale))
        {
            Drawing.Draw(context);
        }
    }

    #endregion
}