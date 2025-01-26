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
        public double Spacing { get; set; } = 5;

        [ConstructorArgument("orientation")]
        public Orientation Orientation { get; set; } = Orientation.Horizontal;

        [ConstructorArgument("text")]
        public string? Text { get; set; }

        [ConstructorArgument("textFirst")]
        public bool TextFirst { get; set; } = false;

        public override object ProvideValue(IServiceProvider serviceProvider) {
            var icon = (Control)base.ProvideValue(serviceProvider);
            
            if (string.IsNullOrWhiteSpace(Text))
                return icon;
            
            var textBlock = new TextBlock {
                Text = Text,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            if (Size.HasValue)
                textBlock.FontSize = Size.Value;

            if (Spacing > 0) {
                if (TextFirst) {
                    textBlock.Margin = Orientation == Orientation.Horizontal 
                        ? new Thickness(0, 0, Spacing, 0) 
                        : new Thickness(0, 0, 0, Spacing);
                }
                else {
                    textBlock.Margin = Orientation == Orientation.Horizontal 
                        ? new Thickness(Spacing, 0, 0, 0) 
                        : new Thickness(0, Spacing, 0, 0);
                }
            }

            return new StackPanel {
                Orientation = Orientation,
                Children = {
                    TextFirst ? textBlock : icon,
                    TextFirst ? icon : textBlock
                }
            };
        }
    }
}
