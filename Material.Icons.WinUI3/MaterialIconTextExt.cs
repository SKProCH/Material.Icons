using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Material.Icons.WinUI3;

public partial class MaterialIconTextExt : MaterialIconExt {
    public MaterialIconTextExt() { }
    public MaterialIconTextExt(MaterialIconKind kind) : base(kind) { }

    public MaterialIconTextExt(MaterialIconKind kind, double size) : base(kind, size) { }

    public double Spacing { get; set; } = 5;

    public Orientation Orientation { get; set; } = Orientation.Horizontal;

    public string? Text { get; set; }

    public bool TextFirst { get; set; } = false;

    protected override object ProvideValue(IXamlServiceProvider serviceProvider) {
        var icon = (Control)base.ProvideValue(serviceProvider);

        if (string.IsNullOrWhiteSpace(Text)) return icon;

        var textBlock = new TextBlock {
            Text = Text,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };

        if (Size.HasValue) textBlock.FontSize = Size.Value;

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
