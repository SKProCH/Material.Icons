using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;

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

    protected override object ProvideValue(IXamlServiceProvider serviceProvider) {
        var result = new MaterialIcon
        {
            Kind = Kind,
            Animation = Animation
        };

        if (IconSize.HasValue) {
            result.Height = IconSize.Value;
            result.Width = IconSize.Value;
        }

        return result;
    }
}
