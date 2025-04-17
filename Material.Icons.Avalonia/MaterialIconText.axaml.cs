using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;

namespace Material.Icons.Avalonia {
    [TemplatePart("PART_LeftIcon", typeof(MaterialIcon))]
    [TemplatePart("PART_RightIcon", typeof(MaterialIcon))]
    public class MaterialIconText : MaterialIcon {
        public static readonly StyledProperty<double> SpacingProperty =
            StackPanel.SpacingProperty.AddOwner<MaterialIconText>();

        public static readonly StyledProperty<Orientation> OrientationProperty =
            StackPanel.OrientationProperty.AddOwner<MaterialIconText>();

        public static readonly StyledProperty<string?> TextProperty =
            TextBlock.TextProperty.AddOwner<MaterialIconText>();

        public static readonly StyledProperty<bool> TextFirstProperty =
            AvaloniaProperty.Register<MaterialIconText, bool>(nameof(TextFirst));

        public static readonly StyledProperty<bool> IsTextSelectableProperty =
            AvaloniaProperty.Register<MaterialIconText, bool>(nameof(IsTextSelectable));

       /// <summary>
        /// Gets or sets the spacing between the icon and the text.
        /// </summary>
        public double Spacing {
            get => GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the orientation in which the icon and the text will be layed out.
        /// </summary>
        public Orientation Orientation {
            get => GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// Gets or sets the text to display
        /// </summary>
        public string? Text {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the text should appear on the left side instead of the right
        /// </summary>
        public bool TextFirst {
            get => GetValue(TextFirstProperty);
            set => SetValue(TextFirstProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the text should be selectable
        /// </summary>
        public bool IsTextSelectable {
            get => GetValue(IsTextSelectableProperty);
            set => SetValue(IsTextSelectableProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
            base.OnApplyTemplate(e);

            // Redirect classses to the left and right icons
            var leftIcon = e.NameScope.Get<MaterialIcon>("PART_LeftIcon");
            var rightIcon = e.NameScope.Get<MaterialIcon>("PART_RightIcon");

            leftIcon.Classes.AddRange(Classes);
            rightIcon.Classes.AddRange(Classes);
        }
    }
}
