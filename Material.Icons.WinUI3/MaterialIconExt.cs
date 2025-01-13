using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

namespace Material.Icons.WinUI3;

[MarkupExtensionReturnType(ReturnType = typeof(MaterialIcon))]
public partial class MaterialIconExt : MarkupExtension {
    public MaterialIconExt() { }

    public MaterialIconExt(MaterialIconKind kind) {
        Kind = kind;
    }

    public MaterialIconExt(MaterialIconKind kind, double size) {
        Kind = kind;
        Size = size;
    }

    public MaterialIconKind Kind { get; set; }

    public double? Size { get; set; }

    public double Spacing { get; set; } = 5;

    public Orientation Orientation { get; set; } = Orientation.Horizontal;

    public string? Text { get; set; }

    public bool TextFirst { get; set; } = false;

    protected override object ProvideValue(IXamlServiceProvider serviceProvider) {
        var icon = new MaterialIcon { Kind = Kind };
        if (Size.HasValue) {
            icon.Height = Size.Value;
            icon.Width = Size.Value;
        }

        if (string.IsNullOrWhiteSpace(Text))
            return icon;

        var textBlock = new TextBlock {
            Text = Text,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };

        if (Size.HasValue)
            textBlock.FontSize = Size.Value;

        return new StackPanel {
            Orientation = Orientation,
            Spacing = Spacing,
            Children = {
                TextFirst ? textBlock : icon,
                TextFirst ? icon : textBlock
            }
        };
    }
}
