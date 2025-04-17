using System;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Material.Icons.WPF
{
    [MarkupExtensionReturnType(typeof(MaterialIcon))]
    public class MaterialIconTextExt : MaterialIconExt {
        public MaterialIconTextExt() { }
        public MaterialIconTextExt(MaterialIconKind kind, MaterialIconAnimation animation = MaterialIconAnimation.None) : base(kind, animation) { }

        public MaterialIconTextExt(MaterialIconKind kind, string? text, double iconSize, MaterialIconAnimation animation = MaterialIconAnimation.None)
            : base(kind, iconSize, animation) {
            Text = text;
        }

        public MaterialIconTextExt(MaterialIconKind kind, double iconSize, string? text = null, MaterialIconAnimation animation = MaterialIconAnimation.None)
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

        public override object ProvideValue(IServiceProvider serviceProvider) {
            if (string.IsNullOrWhiteSpace(Text))
                return base.ProvideValue(serviceProvider);

            var result = new MaterialIconText();
            if (Spacing.HasValue)
                result.Spacing = Spacing.Value;
            if (Orientation.HasValue)
                result.Orientation = Orientation.Value;
            if (TextFirst.HasValue)
                result.TextFirst = TextFirst.Value;
            if (IconSize.HasValue) {
                result.IconSize = IconSize.Value;
                result.FontSize = IconSize.Value;
            }
            result.Kind = Kind;
            result.Text = Text;
            result.Animation = Animation;
            return result;
        }
    }
}
