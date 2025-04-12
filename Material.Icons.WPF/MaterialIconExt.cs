using System;
using System.Windows.Markup;

namespace Material.Icons.WPF {
    [MarkupExtensionReturnType(typeof(MaterialIcon))]
    public class MaterialIconExt : MarkupExtension
    {
        public MaterialIconExt()
        { }

        public MaterialIconExt(MaterialIconKind kind, MaterialIconAnimation animation = MaterialIconAnimation.None) {
            Kind = kind;
            Animation = animation;
        }

        public MaterialIconExt(MaterialIconKind kind, double size, MaterialIconAnimation animation = MaterialIconAnimation.None) {
            Kind = kind;
            Size = size;
            Animation = animation;
        }

        [ConstructorArgument("kind")]
        public MaterialIconKind Kind { get; set; }

        [ConstructorArgument("animation")]
        public MaterialIconAnimation Animation { get; set; }

        [ConstructorArgument("size")]
        public double? Size { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var result = new MaterialIcon
            {
                Kind = Kind,
                Animation = Animation
            };

            if (Size.HasValue)
            {
                result.Height = Size.Value;
                result.Width = Size.Value;
            }

            return result;
        }
    }
}
