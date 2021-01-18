using System;
using Avalonia.Markup.Xaml;

namespace Material.Icons.Avalonia {
    public class MaterialIconExt : MarkupExtension {
        public MaterialIconExt() { }
        public MaterialIconExt(MaterialIconKind kind) {
            Kind = kind;
        }

        public MaterialIconExt(MaterialIconKind kind, double? size) {
            Kind = kind;
            Size = size;
        }
        
        [ConstructorArgument("kind")]
        public MaterialIconKind Kind { get; set; }

        [ConstructorArgument("size")]
        public double? Size { get; set; }
        
        public override object ProvideValue(IServiceProvider serviceProvider) {
            var result = new MaterialIcon { Kind = Kind };

            if (Size.HasValue)
            {
                result.Height = Size.Value;
                result.Width = Size.Value;
            }

            return result;
        }
    }
}