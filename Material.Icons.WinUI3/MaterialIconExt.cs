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

    public MaterialIconExt(MaterialIconKind kind, double? size, MaterialIconAnimation animation = MaterialIconAnimation.None) {
        Kind = kind;
        Size = size;
        Animation = animation;
    }

    public MaterialIconKind Kind { get; set; }

    public MaterialIconAnimation Animation { get; set; }

    public double? Size { get; set; }

    protected override object ProvideValue(IXamlServiceProvider serviceProvider) {
        var result = new MaterialIcon
        {
            Kind = Kind,
            Animation = Animation
        };

        if (Size.HasValue) {
            result.Height = Size.Value;
            result.Width = Size.Value;
        }

        return result;
    }
}
