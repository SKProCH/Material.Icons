using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Material.Icons.WinUI3;

public partial class MaterialIconTextExt : MaterialIconExt {
    public MaterialIconTextExt() { }
    public MaterialIconTextExt(MaterialIconKind kind) : base(kind) { }

    public MaterialIconTextExt(MaterialIconKind kind, double size) : base(kind, size) { }

    public double? Spacing { get; set; }

    public Orientation? Orientation { get; set; }

    public string? Text { get; set; }

    public bool? TextFirst { get; set; }

    protected override object ProvideValue(IXamlServiceProvider serviceProvider) {
        if (string.IsNullOrWhiteSpace(Text))
            return base.ProvideValue(serviceProvider);
        var result = new MaterialIconText();
        if (Spacing.HasValue)
            result.Spacing = Spacing.Value;
        if (Orientation.HasValue)
            result.Orientation = Orientation.Value;
        if (TextFirst.HasValue)
            result.TextFirst = TextFirst.Value;
        result.Text = Text;
        return result;
    }
}
