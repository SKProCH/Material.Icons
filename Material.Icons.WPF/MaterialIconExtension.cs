using System;
using System.Windows.Markup;

namespace Material.Icons.WPF {
    [MarkupExtensionReturnType(typeof(MaterialIcon))]
    public class PackIconExtension : MarkupExtension
    {
        public PackIconExtension()
        { }

        public PackIconExtension(MaterialIconKind kind)
        {
            Kind = kind;
        }

        public PackIconExtension(MaterialIconKind kind, double size)
        {
            Kind = kind;
            Size = size;
        }

        [ConstructorArgument("kind")]
        public MaterialIconKind Kind { get; set; }

        [ConstructorArgument("size")]
        public double? Size { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
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