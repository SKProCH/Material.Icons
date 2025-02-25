using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

namespace Material.Icons.Avalonia
{
    public class MaterialIconTextExt : MaterialIconExt {
        public MaterialIconTextExt() { }
        public MaterialIconTextExt(MaterialIconKind kind) : base(kind) { }

        public MaterialIconTextExt(MaterialIconKind kind, double? size) : base(kind, size) { }

        [ConstructorArgument("spacing")]
        public double? Spacing { get; set; } = 5;

        [ConstructorArgument("orientation")]
        public Orientation? Orientation { get; set; } = global::Avalonia.Layout.Orientation.Horizontal;

        [ConstructorArgument("text")]
        public string? Text { get; set; }

        [ConstructorArgument("textFirst")]
        public bool? TextFirst { get; set; } = false;

        [ConstructorArgument("isTextSelectable")]
        public bool? IsTextSelectable { get; set; } = false;
        
        public override object ProvideValue(IServiceProvider serviceProvider) {
            if (string.IsNullOrWhiteSpace(Text)) return (Control)base.ProvideValue(serviceProvider);
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
            return result;
        }
    }
}
