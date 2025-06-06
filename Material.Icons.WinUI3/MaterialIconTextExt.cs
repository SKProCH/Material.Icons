using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Material.Icons.WinUI3;

public partial class MaterialIconTextExt : MaterialIconExt {
    public MaterialIconTextExt() { }
    public MaterialIconTextExt(MaterialIconKind kind) : base(kind) {
    }

    public MaterialIconTextExt(MaterialIconKind kind, MaterialIconAnimation animation) : base(kind, animation) {
    }

    public MaterialIconTextExt(MaterialIconKind kind, double iconSize, MaterialIconAnimation animation = MaterialIconAnimation.None) : base(kind, iconSize, animation) {
    }

    public MaterialIconTextExt(MaterialIconKind kind, string text, MaterialIconAnimation animation = MaterialIconAnimation.None)
        : base(kind, animation) {
        Text = text;
    }

    public MaterialIconTextExt(MaterialIconKind kind, double iconSize, string text, MaterialIconAnimation animation = MaterialIconAnimation.None)
        : base(kind, iconSize, animation) {
        Text = text;
    }


    public double? Spacing { get; set; }

    public Orientation? Orientation { get; set; }

    public string? Text { get; set; }

    public bool? TextFirst { get; set; }

    public VerticalAlignment? VerticalContentAlignment { get; set; }

    public HorizontalAlignment? HorizontalContentAlignment { get; set; }

    protected override object ProvideValue(IXamlServiceProvider serviceProvider) {
        if (string.IsNullOrWhiteSpace(Text))
            return base.ProvideValue(serviceProvider);

        var result = new MaterialIconText {
            Kind = Kind,
            Text = Text,
            Animation = Animation
        };

        if (IconSize is not null) result.IconSize = IconSize.Value;
        if (IconForeground is not null) result.Foreground = IconForeground;

        if (Spacing is not null) result.Spacing = Spacing.Value;
        if (Orientation is not null) result.Orientation = Orientation.Value;
        if (TextFirst is not null) result.TextFirst = TextFirst.Value;

        if (VerticalAlignment is not null) result.VerticalAlignment = VerticalAlignment.Value;
        if (HorizontalAlignment is not null) result.HorizontalAlignment = HorizontalAlignment.Value;
        if (VerticalContentAlignment is not null) result.VerticalContentAlignment = VerticalContentAlignment.Value;
        if (HorizontalContentAlignment is not null) result.HorizontalContentAlignment = HorizontalContentAlignment.Value;

        return result;
    }
}
