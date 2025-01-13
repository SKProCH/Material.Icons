using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Material.Icons.WPF {
    [MarkupExtensionReturnType(typeof(MaterialIcon))]
    public class MaterialIconExt : MarkupExtension
    {
        public MaterialIconExt()
        { }

        public MaterialIconExt(MaterialIconKind kind)
        {
            Kind = kind;
        }

        public MaterialIconExt(MaterialIconKind kind, double size)
        {
            Kind = kind;
            Size = size;
        }

        [ConstructorArgument("kind")]
        public MaterialIconKind Kind { get; set; }

        [ConstructorArgument("size")]
        public double? Size { get; set; }

        [ConstructorArgument("spacing")]
        public double Spacing { get; set; } = 5;

        [ConstructorArgument("orientation")]
        public Orientation Orientation { get; set; } = Orientation.Horizontal;

        [ConstructorArgument("text")]
        public string? Text { get; set; }

        [ConstructorArgument("textFirst")]
        public bool TextFirst { get; set; } = false;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var icon = new MaterialIcon { Kind = Kind };
            if (Size.HasValue) {
                icon.Height = Size.Value;
                icon.Width = Size.Value;
            }
            
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
