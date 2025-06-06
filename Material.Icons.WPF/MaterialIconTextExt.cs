using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Material.Icons.WPF
{
    [MarkupExtensionReturnType(typeof(MaterialIcon))]
    public class MaterialIconTextExt : MaterialIconExt {
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

        [ConstructorArgument("spacing")]
        public double? Spacing { get; set; }

        [ConstructorArgument("orientation")]
        public Orientation? Orientation { get; set; }

        [ConstructorArgument("text")]
        public string? Text { get; set; }

        [ConstructorArgument("textFirst")]
        public bool? TextFirst { get; set; }

        [ConstructorArgument("verticalContentAlignment")]
        public VerticalAlignment? VerticalContentAlignment { get; set; }

        [ConstructorArgument("horizontalContentAlignment")]
        public HorizontalAlignment? HorizontalContentAlignment { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            if (string.IsNullOrWhiteSpace(Text))
                return base.ProvideValue(serviceProvider);

            var result = new MaterialIconText {
                Kind = Kind,
                Text = Text,
                Animation = Animation
            };

            if (IconSize is not null) {
                result.IconSize = IconSize.Value;
                result.FontSize = IconSize.Value;
            }
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
}
