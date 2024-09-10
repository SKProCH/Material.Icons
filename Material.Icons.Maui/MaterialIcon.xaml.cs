using System.ComponentModel;
using System.Diagnostics;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Maui.Controls.Shapes;

namespace Material.Icons.Maui;

public partial class MaterialIcon {
    private static readonly BindablePropertyKey DataPropertyKey
        = BindableProperty.CreateReadOnly(nameof(Data), typeof(Geometry), typeof(MaterialIcon), null);

    public static readonly BindableProperty KindProperty
        = BindableProperty.Create(nameof(Kind), typeof(MaterialIconKind?), typeof(MaterialIcon),
            propertyChanged: KindPropertyChangedCallback);
    
    public static readonly BindableProperty ForegroundProperty
        = BindableProperty.Create(nameof(Foreground), typeof(Brush), typeof(MaterialIcon));

    public static readonly BindableProperty DataProperty = DataPropertyKey.BindableProperty;

    private readonly PathGeometryConverter _converter = new();
    
    private ILogger<MaterialIcon>? _logger;

    public MaterialIcon()
	{
		InitializeComponent();
    }

    /// <summary>
    /// Gets or sets the brush used to draw the icon.
    /// </summary>
    [TypeConverter(typeof(BrushTypeConverter))]
    public Brush? Foreground {
        get => (Brush?)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon to display.
    /// </summary>
    public MaterialIconKind? Kind {
        get => (MaterialIconKind?)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    /// <summary>
    /// Gets the icon path data for the current <see cref="Kind"/>.
    /// </summary>
    public Geometry? Data => (Geometry?)GetValue(DataProperty);
    
    private ILogger<MaterialIcon> Logger => _logger ??= GetLogger() ?? NullLogger<MaterialIcon>.Instance;

    private static void KindPropertyChangedCallback(BindableObject bindable, object oldValue, object newValue) {
        ((MaterialIcon)bindable).UpdateData((MaterialIconKind?)newValue);
    }
    
    private MaterialIconDataProvider GetProvider() {
        return IPlatformApplication.Current?.Services.GetService<MaterialIconDataProvider>()
               ?? MaterialIconDataProvider.Instance;
    }
    
    private ILogger<MaterialIcon>? GetLogger() {
        return IPlatformApplication.Current?.Services.GetService<ILogger<MaterialIcon>>();
    }
    
    private Geometry? GetGeometry(MaterialIconKind? kind) {
        try {
            Debug.WriteLine($"Getting geometry for icon {kind}");
            var result = kind is not null
                ? (Geometry?)_converter.ConvertFromInvariantString(GetProvider().ProvideData(kind.Value))
                : null;
            Debug.WriteLine($"Got geometry for icon {kind}");
            return result;
        }
        catch (Exception ex) {
            Logger.LogError(ex, "Failed to get geometry for icon {Kind}", kind);
            return null;
        }
    }

    private void UpdateData(MaterialIconKind? kind) {
        SetValue(DataPropertyKey, GetGeometry(kind));
    }
}
