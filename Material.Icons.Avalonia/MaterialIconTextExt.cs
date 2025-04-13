using System;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

namespace Material.Icons.Avalonia
{
    public class MaterialIconTextExt : MaterialIconExt {
        public MaterialIconTextExt() { }
        public MaterialIconTextExt(MaterialIconKind kind, MaterialIconAnimation animation = MaterialIconAnimation.None) : base(kind, animation) { }

        public MaterialIconTextExt(MaterialIconKind kind, string? text, double? size = null, MaterialIconAnimation animation = MaterialIconAnimation.None)
            : base(kind, size, animation)
        {
            Text = text;
        }

        public MaterialIconTextExt(MaterialIconKind kind, double? size, string? text = null, MaterialIconAnimation animation = MaterialIconAnimation.None)
            : base(kind, size, animation)
        {
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

        [ConstructorArgument("isTextSelectable")]
        public bool? IsTextSelectable { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            if (string.IsNullOrWhiteSpace(Text))
                return base.ProvideValue(serviceProvider);

            var result = new MaterialIconText();
            if (Spacing.HasValue) result.Spacing = Spacing.Value;
            if (Orientation.HasValue) result.Orientation = Orientation.Value;
            if (TextFirst.HasValue) result.TextFirst = TextFirst.Value;
            if (IsTextSelectable.HasValue) result.IsTextSelectable = IsTextSelectable.Value;
            if (Size.HasValue) {
                result.IconSize = Size.Value;
                result.FontSize = Size.Value;
            }
            result.Kind = Kind;
            result.Text = Text;
            result.Animation = Animation;
            return result;
        }
    }
}
