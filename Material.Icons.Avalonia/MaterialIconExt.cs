using System;
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

        [ConstructorArgument("iconBrush")]
        public IBrush? IconBrush { get; set; }

        [ConstructorArgument("animation")]
        public MaterialIconAnimation Animation { get; set; }

        [ConstructorArgument("classes")]
        public string? Classes { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            var result = new MaterialIcon {
                Kind = Kind,
                Animation = Animation
            };

            if (IconSize.HasValue) {
                result.IconSize = IconSize.Value;
            }

            if (IconBrush is not null) {
                result.Foreground = IconBrush;
            }

            if (!string.IsNullOrWhiteSpace(Classes)) {
                result.Classes.AddRange(global::Avalonia.Controls.Classes.Parse(Classes!));
            }

            return result;
        }
    }
}
