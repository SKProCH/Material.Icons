using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Material.Icons.WinUI3;

public partial class MaterialIconTextExt : MaterialIconExt {
    public MaterialIconTextExt() { }
    public MaterialIconTextExt(MaterialIconKind kind, MaterialIconAnimation animation = MaterialIconAnimation.None) : base(kind, animation) { }

    public MaterialIconTextExt(MaterialIconKind kind, string? text, double? iconSize = null, MaterialIconAnimation animation = MaterialIconAnimation.None)
        : base(kind, iconSize, animation)
    {
        Text = text;
    }

    public MaterialIconTextExt(MaterialIconKind kind, double? iconSize, string? text = null, MaterialIconAnimation animation = MaterialIconAnimation.None)
        : base(kind, iconSize, animation)
    {
        Text = text;
    }

    public double? Spacing { get; set; }

    public Orientation? Orientation { get; set; }

    public string? Text { get; set; }

    public bool? TextFirst { get; set; }

    protected override object ProvideValue(IXamlServiceProvider serviceProvider) {
        if (string.IsNullOrWhiteSpace(Text))
            return base.ProvideValue(serviceProvider);

        var result = new MaterialIconText {
            Kind = Kind,
            Text = Text,
            Animation = Animation
        };

        if (IconSize.HasValue) result.IconSize = IconSize.Value;
        if (IconForeground is not null) result.Foreground = IconForeground;

        if (Spacing.HasValue) result.Spacing = Spacing.Value;
        if (Orientation.HasValue) result.Orientation = Orientation.Value;
        if (TextFirst.HasValue) result.TextFirst = TextFirst.Value;

        return result;
    }
}
