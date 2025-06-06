using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

namespace Material.Icons.Avalonia
{
    public class MaterialIconTextExt : MaterialIconExt {
        public MaterialIconTextExt() { }

        public MaterialIconTextExt(MaterialIconKind kind) : base(kind)
        {
        }

        public MaterialIconTextExt(MaterialIconKind kind, MaterialIconAnimation animation) : base(kind, animation)
        {
        }

        public MaterialIconTextExt(MaterialIconKind kind, double iconSize, MaterialIconAnimation animation = MaterialIconAnimation.None) : base(kind, iconSize, animation)
        {
        }

        public MaterialIconTextExt(MaterialIconKind kind, string text, MaterialIconAnimation animation = MaterialIconAnimation.None)
            : base(kind, animation)
        {
            Text = text;
        }

        public MaterialIconTextExt(MaterialIconKind kind, double iconSize, string text, MaterialIconAnimation animation = MaterialIconAnimation.None)
            : base(kind, iconSize, animation)
        {
            Text = text;
        }

        public MaterialIconTextExt(MaterialIconKind kind, double iconSize, Dock iconPlacement, string text, MaterialIconAnimation animation = MaterialIconAnimation.None)
            : base(kind, iconSize, animation) {
            IconPlacement = iconPlacement;
            Text = text;
        }

        [ConstructorArgument("iconPlacement")]
        public Dock? IconPlacement { get; set; }

        [ConstructorArgument("spacing")]
        public double? Spacing { get; set; }

        [ConstructorArgument("text")]
        public string? Text { get; set; }

        [ConstructorArgument("isTextSelectable")]
        public bool? IsTextSelectable { get; set; }

        [ConstructorArgument("verticalContentAlignment")]
        public VerticalAlignment? VerticalContentAlignment { get; set; }

        [ConstructorArgument("horizontalContentAlignment")]
        public HorizontalAlignment? HorizontalContentAlignment { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            if (string.IsNullOrWhiteSpace(Text))
                return base.ProvideValue(serviceProvider);

            var result = new MaterialIconText {
                Kind = Kind,
                Text = Text,
                Animation = Animation
            };

            if (IconSize is not null) result.IconSize = IconSize.Value;
            if (IconForeground is not null) result.Foreground = IconForeground;
            if (IconPlacement is not null) result.IconPlacement = IconPlacement.Value;

            if (Spacing is not null) result.Spacing = Spacing.Value;
            if (IsTextSelectable is not null) result.IsTextSelectable = IsTextSelectable.Value;

            if (VerticalAlignment is not null) result.VerticalAlignment = VerticalAlignment.Value;
            if (HorizontalAlignment is not null) result.HorizontalAlignment = HorizontalAlignment.Value;
            if (VerticalContentAlignment is not null) result.VerticalContentAlignment = VerticalContentAlignment.Value;
            if (HorizontalContentAlignment is not null) result.HorizontalContentAlignment = HorizontalContentAlignment.Value;

            if (!string.IsNullOrWhiteSpace(Classes)) {
                result.Classes.AddRange(global::Avalonia.Controls.Classes.Parse(Classes!));
            }

            return result;
        }
    }
}
