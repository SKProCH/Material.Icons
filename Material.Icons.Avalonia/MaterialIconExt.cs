using System;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Material.Icons.Avalonia {
    public class MaterialIconExt : MarkupExtension {
        public MaterialIconExt() { }
        public MaterialIconExt(MaterialIconKind kind, MaterialIconAnimation animation = MaterialIconAnimation.None, string? classes = null) {
            Kind = kind;
            Animation = animation;
            Classes = classes;
        }

        public MaterialIconExt(MaterialIconKind kind, double? iconSize, MaterialIconAnimation animation = MaterialIconAnimation.None, string? classes = null) {
            Kind = kind;
            IconSize = iconSize;
            Animation = animation;
            Classes = classes;
        }

        [ConstructorArgument("kind")]
        public MaterialIconKind Kind { get; set; }

        [ConstructorArgument("iconSize")]
        public double? IconSize { get; set; }

        [ConstructorArgument("iconForeground")]
        public IBrush? IconForeground { get; set; }

        [ConstructorArgument("animation")]
        public MaterialIconAnimation Animation { get; set; }

        [ConstructorArgument("verticalAlignment")]
        public VerticalAlignment? VerticalAlignment { get; set; }

        [ConstructorArgument("horizontalAlignment")]
        public HorizontalAlignment? HorizontalAlignment { get; set; }

        [ConstructorArgument("classes")]
        public string? Classes { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            var result = new MaterialIcon {
                Kind = Kind,
                Animation = Animation
            };

            if (IconSize is not null) result.IconSize = IconSize.Value;
            if (IconForeground is not null) result.Foreground = IconForeground;

            if (VerticalAlignment is not null) result.VerticalAlignment = VerticalAlignment.Value;
            if (HorizontalAlignment is not null) result.HorizontalAlignment = HorizontalAlignment.Value;

            if (!string.IsNullOrWhiteSpace(Classes)) {
                result.Classes.AddRange(global::Avalonia.Controls.Classes.Parse(Classes!));
            }

            return result;
        }
    }
}
