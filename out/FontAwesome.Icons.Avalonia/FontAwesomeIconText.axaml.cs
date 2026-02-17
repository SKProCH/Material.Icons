using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;

namespace FontAwesome.Icons.Avalonia; 

[TemplatePart("PART_Icon", typeof(FontAwesomeIcon))]
[TemplatePart("PART_TextBlock", typeof(TextBlock))]
[TemplatePart("PART_SelectableTextBlock", typeof(SelectableTextBlock))]
public class FontAwesomeIconText : FontAwesomeIcon {
    public static readonly StyledProperty<Dock> IconPlacementProperty =
        DockPanel.DockProperty.AddOwner<FontAwesomeIconText>();

    public static readonly StyledProperty<double> SpacingProperty =
        StackPanel.SpacingProperty.AddOwner<FontAwesomeIconText>();

    public static readonly StyledProperty<Orientation> OrientationProperty =
        StackPanel.OrientationProperty.AddOwner<FontAwesomeIconText>();

    public static readonly StyledProperty<string?> TextProperty =
        TextBlock.TextProperty.AddOwner<FontAwesomeIconText>();

    public static readonly StyledProperty<bool> IsTextSelectableProperty =
        AvaloniaProperty.Register<FontAwesomeIconText, bool>(nameof(IsTextSelectable));

    /// <summary>
    /// Defines the <see cref="HorizontalContentAlignment"/> property.
    /// </summary>
    public static readonly StyledProperty<HorizontalAlignment> HorizontalContentAlignmentProperty =
        ContentControl.HorizontalContentAlignmentProperty.AddOwner<FontAwesomeIconText>();

    /// <summary>
    /// Defines the <see cref="VerticalContentAlignment"/> property.
    /// </summary>
    public static readonly StyledProperty<VerticalAlignment> VerticalContentAlignmentProperty =
        ContentControl.VerticalContentAlignmentProperty.AddOwner<FontAwesomeIconText>();

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


        if (Classes.Count > 0) {
            // Ignore pseudo-classes replication, otherwise crash with: The pseudoclass ':xxx' may only be added by the control itself.
            var filteredClasses = Classes.Where(c => c.Length > 0 && c[0] != ':').ToArray();

            if (filteredClasses.Length > 0) {
                // Redirect classes to the template parts
                var icon = e.NameScope.Get<FontAwesomeIcon>("PART_Icon");
                var textBlock = e.NameScope.Get<TextBlock>("PART_TextBlock");
                var selectableTextBlock = e.NameScope.Get<SelectableTextBlock>("PART_SelectableTextBlock");

                icon.Classes.AddRange(filteredClasses);
                textBlock.Classes.AddRange(filteredClasses);
                selectableTextBlock.Classes.AddRange(filteredClasses);
            }
        }
    }
}
