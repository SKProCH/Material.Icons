using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace Material.Icons.WPF {
    [MarkupExtensionReturnType(typeof(MaterialIcon))]
    public class MaterialIconExt : MarkupExtension
    {
        public MaterialIconExt()
        { }

        public MaterialIconExt(MaterialIconKind kind) {
            Kind = kind;
        }

        public MaterialIconExt(MaterialIconKind kind, MaterialIconAnimation animation) {
            Kind = kind;
            Animation = animation;
        }

        public MaterialIconExt(MaterialIconKind kind, double iconSize, MaterialIconAnimation animation = MaterialIconAnimation.None) {
            Kind = kind;
            IconSize = iconSize;
            Animation = animation;
        }

        [ConstructorArgument("kind")]
        public MaterialIconKind Kind { get; set; }

        [ConstructorArgument("animation")]
        public MaterialIconAnimation Animation { get; set; }

        [ConstructorArgument("iconSize")]
        public double? IconSize { get; set; }

        [ConstructorArgument("iconForeground")]
        public Brush? IconForeground { get; set; }

        [ConstructorArgument("verticalAlignment")]
        public VerticalAlignment? VerticalAlignment { get; set; }

        [ConstructorArgument("horizontalAlignment")]
        public HorizontalAlignment? HorizontalAlignment { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var result = new MaterialIcon
            {
                Kind = Kind,
                Animation = Animation
            };

            if (IconSize is not null)
            {
                result.Height = IconSize.Value;
                result.Width = IconSize.Value;
            }

            if (IconForeground is not null) result.Foreground = IconForeground;

            if (VerticalAlignment is not null) result.VerticalAlignment = VerticalAlignment.Value;
            if (HorizontalAlignment is not null) result.HorizontalAlignment = HorizontalAlignment.Value;

            return result;
        }
    }
}
