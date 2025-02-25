using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Material.Icons.WPF
{
    [MarkupExtensionReturnType(typeof(MaterialIcon))]
    public class MaterialIconTextExt : MaterialIconExt {
        public MaterialIconTextExt() { }
        public MaterialIconTextExt(MaterialIconKind kind) : base(kind) { }

        public MaterialIconTextExt(MaterialIconKind kind, double size) : base(kind, size) { }

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
            if (Size.HasValue) {
                result.IconSize = Size.Value;
                result.FontSize = Size.Value;
            }
            result.Text = Text;
            return result;
        }
    }
}
