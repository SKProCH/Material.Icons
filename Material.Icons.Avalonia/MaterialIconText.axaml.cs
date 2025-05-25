using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;

namespace Material.Icons.Avalonia {
    [TemplatePart("PART_Icon", typeof(MaterialIcon))]
    [TemplatePart("PART_TextBlock", typeof(TextBlock))]
    [TemplatePart("PART_SelectableTextBlock", typeof(SelectableTextBlock))]
    public class MaterialIconText : MaterialIcon {
        public static readonly StyledProperty<Dock> IconPlacementProperty =
            DockPanel.DockProperty.AddOwner<MaterialIconText>();

        public static readonly StyledProperty<double> SpacingProperty =
            StackPanel.SpacingProperty.AddOwner<MaterialIconText>();

        public static readonly StyledProperty<Orientation> OrientationProperty =
            StackPanel.OrientationProperty.AddOwner<MaterialIconText>();

        public static readonly StyledProperty<string?> TextProperty =
            TextBlock.TextProperty.AddOwner<MaterialIconText>();

        public static readonly StyledProperty<bool> IsTextSelectableProperty =
            AvaloniaProperty.Register<MaterialIconText, bool>(nameof(IsTextSelectable));

        /// <summary>
        /// Defines the <see cref="HorizontalContentAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<HorizontalAlignment> HorizontalContentAlignmentProperty =
            ContentControl.HorizontalContentAlignmentProperty.AddOwner<MaterialIconText>();

        /// <summary>
        /// Defines the <see cref="VerticalContentAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<VerticalAlignment> VerticalContentAlignmentProperty =
            ContentControl.VerticalContentAlignmentProperty.AddOwner<MaterialIconText>();

        /// <summary>
        /// Gets or sets the icon placement relative to the text.
        /// </summary>
        public Dock IconPlacement {
            get => GetValue(IconPlacementProperty);
            set => SetValue(IconPlacementProperty, value);
        }

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
        /// Gets or sets whether the text should be selectable
        /// </summary>
        public bool IsTextSelectable {
            get => GetValue(IsTextSelectableProperty);
            set => SetValue(IsTextSelectableProperty, value);
        }

        /// <summary>
        /// Gets or sets the horizontal alignment of the content within the control.
        /// </summary>
        public HorizontalAlignment HorizontalContentAlignment {
            get => GetValue(HorizontalContentAlignmentProperty);
            set => SetValue(HorizontalContentAlignmentProperty, value);
        }

        /// <summary>
        /// Gets or sets the vertical alignment of the content within the control.
        /// </summary>
        public VerticalAlignment VerticalContentAlignment {
            get => GetValue(VerticalContentAlignmentProperty);
            set => SetValue(VerticalContentAlignmentProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
            base.OnApplyTemplate(e);

            var classes = Classes;
            if (classes.Count > 0) {

                // Redirect classses to the template parts
                var icon = e.NameScope.Get<MaterialIcon>("PART_Icon");
                var textBlock = e.NameScope.Get<TextBlock>("PART_TextBlock");
                var selectableTextBlock = e.NameScope.Get<SelectableTextBlock>("PART_SelectableTextBlock");

                icon.Classes.AddRange(classes);
                textBlock.Classes.AddRange(classes);
                selectableTextBlock.Classes.AddRange(classes);
            }
        }
    }
}
