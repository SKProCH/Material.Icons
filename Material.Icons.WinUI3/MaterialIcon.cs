using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Material.Icons.WinUI3;

public partial class MaterialIcon : Control {
    public static readonly DependencyProperty KindProperty
        = DependencyProperty.Register(nameof(Kind), typeof(MaterialIconKind), typeof(MaterialIcon),
            new PropertyMetadata(default(MaterialIconKind), KindPropertyChangedCallback));

    public static readonly DependencyProperty AnimationProperty
        = DependencyProperty.Register(nameof(Animation), typeof(MaterialIconAnimation), typeof(MaterialIcon),
            new PropertyMetadata(default(MaterialIconAnimation)));

    public static readonly DependencyProperty DataProperty
        = DependencyProperty.Register(nameof(Data), typeof(Geometry), typeof(MaterialIcon), new PropertyMetadata(null));

    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register(
        nameof(IconSize), typeof(double), typeof(MaterialIcon), new PropertyMetadata(double.NaN));

    static MaterialIcon() {
        MaterialIconsUtils.InitializeGeometryParser();
    }

    public MaterialIcon() {
        DefaultStyleKey = typeof(MaterialIcon);
    }

    /// <summary>
    /// Gets or sets the icon to display.
    /// </summary>
    public MaterialIconKind Kind {
        get => (MaterialIconKind)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon animation to play.
    /// </summary>
    public MaterialIconAnimation Animation {
        get => (MaterialIconAnimation)GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }

    /// <summary>
    /// Gets the icon path data for the current <see cref="Kind"/>.
    /// </summary>
    public Geometry? Data {
        get => (Geometry?)GetValue(DataProperty);
        private set => SetValue(DataProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the icon size
    /// </summary>
    public double IconSize {
        get { return (double)GetValue(IconSizeProperty); }
        set { SetValue(IconSizeProperty, value); }
    }

    protected override void OnApplyTemplate() {
        base.OnApplyTemplate();
        UpdateData();
    }

    private static void KindPropertyChangedCallback(DependencyObject dependencyObject,
        DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        => ((MaterialIcon)dependencyObject).UpdateData();

    private void UpdateData() {
        Data = MaterialIconParser.Parse(MaterialIconDataProvider.GetData(Kind));
    }
}
