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
        public double Spacing { get; set; } = 5;

        [ConstructorArgument("orientation")]
        public Orientation Orientation { get; set; } = Orientation.Horizontal;

        [ConstructorArgument("text")]
        public string? Text { get; set; }

        [ConstructorArgument("textFirst")]
        public bool TextFirst { get; set; } = false;

        [ConstructorArgument("isTextSelectable")]
        public bool IsTextSelectable { get; set; } = false;
        
        public override object ProvideValue(IServiceProvider serviceProvider) {
            var icon = (Control)base.ProvideValue(serviceProvider);

            if (string.IsNullOrWhiteSpace(Text)) return icon;

            var textBlock = IsTextSelectable ?
                new SelectableTextBlock {
                    Text = Text,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                }
                : new TextBlock {
                    Text = Text,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };

            if (Size.HasValue) textBlock.FontSize = Size.Value;

            return new StackPanel {
                Orientation = Orientation,
                Spacing = Spacing,
                Children = {
                    TextFirst ? textBlock : icon,
                    TextFirst ? icon : textBlock
                }
            };
        }
    }
}
