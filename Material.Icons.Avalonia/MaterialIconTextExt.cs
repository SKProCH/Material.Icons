using System;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

namespace Material.Icons.Avalonia
{
    public class MaterialIconTextExt : MaterialIconExt {
        public MaterialIconTextExt() { }
        public MaterialIconTextExt(MaterialIconKind kind, MaterialIconAnimation animation = MaterialIconAnimation.None, string? classes = null) : base(kind, animation, classes) { }

        public MaterialIconTextExt(MaterialIconKind kind, string? text, double? iconSize = null, MaterialIconAnimation animation = MaterialIconAnimation.None, string? classes = null)
            : base(kind, iconSize, animation, classes)
        {
            Text = text;
        }

        public MaterialIconTextExt(MaterialIconKind kind, double? iconSize, string? text = null, MaterialIconAnimation animation = MaterialIconAnimation.None, string? classes = null)
            : base(kind, iconSize, animation, classes)
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

            var result = new MaterialIconText {
                Kind = Kind,
                Text = Text,
                Animation = Animation
            };

            if (IconSize.HasValue) result.IconSize = IconSize.Value;
            if (IconBrush is not null) result.Foreground = IconBrush;

            if (Spacing.HasValue) result.Spacing = Spacing.Value;
            if (Orientation.HasValue) result.Orientation = Orientation.Value;
            if (TextFirst.HasValue) result.TextFirst = TextFirst.Value;
            if (IsTextSelectable.HasValue) result.IsTextSelectable = IsTextSelectable.Value;

            if (!string.IsNullOrWhiteSpace(Classes)) {
                result.Classes.AddRange(global::Avalonia.Controls.Classes.Parse(Classes!));
            }

            return result;
        }
    }
}
