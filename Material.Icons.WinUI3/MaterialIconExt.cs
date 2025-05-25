using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;

namespace Material.Icons.WinUI3;

[MarkupExtensionReturnType(ReturnType = typeof(MaterialIcon))]
public partial class MaterialIconExt : MarkupExtension {
    public MaterialIconExt() { }

    public MaterialIconExt(MaterialIconKind kind, MaterialIconAnimation animation = MaterialIconAnimation.None) {
        Kind = kind;
        Animation = animation;
    }

    public MaterialIconExt(MaterialIconKind kind, double? iconSize, MaterialIconAnimation animation = MaterialIconAnimation.None) {
        Kind = kind;
        IconSize = iconSize;
        Animation = animation;
    }

    public MaterialIconKind Kind { get; set; }

    public MaterialIconAnimation Animation { get; set; }

    public double? IconSize { get; set; }

    public Brush? IconForeground { get; set; }

    public VerticalAlignment? VerticalAlignment { get; set; }

    public HorizontalAlignment? HorizontalAlignment { get; set; }

    protected override object ProvideValue(IXamlServiceProvider serviceProvider) {
        var result = new MaterialIcon
        {
            Kind = Kind,
            Animation = Animation
        };

        if (IconSize is not null) {
            result.Height = IconSize.Value;
            result.Width = IconSize.Value;
        }

        if (IconForeground is not null) result.Foreground = IconForeground;

        if (VerticalAlignment is not null) result.VerticalAlignment = VerticalAlignment.Value;
        if (HorizontalAlignment is not null) result.HorizontalAlignment = HorizontalAlignment.Value;

        return result;
    }
}
