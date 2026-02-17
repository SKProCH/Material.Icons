using System;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Lucide;

namespace Lucide.Icons.Avalonia; 
public class LucideIconExt : MarkupExtension {
    public LucideIconExt() { }
    public LucideIconExt(LucideIconKind kind) {
        Kind = kind;
    }

    public LucideIconExt(LucideIconKind kind, LucideIconAnimation animation) {
        Kind = kind;
        Animation = animation;
    }

    public LucideIconExt(LucideIconKind kind, double iconSize, LucideIconAnimation animation = LucideIconAnimation.None) {
        Kind = kind;
        IconSize = iconSize;
        Animation = animation;
    }

    /// <summary>
    /// Gets or sets the icon to display. Provides IntelliSense autocomplete.
    /// </summary>
    [ConstructorArgument("kind")]
    public LucideIconKind Kind { get; set; }

    /// <summary>
    /// Gets or sets a binding for the icon kind. Use this when data binding is required.
    /// </summary>
    [ConstructorArgument("kindBinding")]
    public IBinding? KindBinding { get; set; }

    /// <summary>
    /// Gets or sets the size of the icon to display.<br/>
    /// Can be a double or a binding.
    /// </summary>
    [ConstructorArgument("iconSize")]
    public object? IconSize { get; set; }

    /// <summary>
    /// Gets or sets the icon foreground brush.
    /// </summary>
    [ConstructorArgument("iconForeground")]
    public IBrush? IconForeground { get; set; }

    /// <summary>
    /// Gets or sets a binding for the icon foreground. Use this when data binding is required.
    /// </summary>
    [ConstructorArgument("iconForegroundBinding")]
    public IBinding? IconForegroundBinding { get; set; }

    /// <summary>
    /// Gets or sets the animation to play. Provides IntelliSense autocomplete.
    /// </summary>
    [ConstructorArgument("animation")]
    public LucideIconAnimation Animation { get; set; }

    /// <summary>
    /// Gets or sets a binding for the animation. Use this when data binding is required.
    /// </summary>
    [ConstructorArgument("animationBinding")]
    public IBinding? AnimationBinding { get; set; }

    /// <summary>
    /// Gets or sets the vertical alignment of the content.
    /// </summary>
    [ConstructorArgument("verticalAlignment")]
    public VerticalAlignment? VerticalAlignment { get; set; }

    /// <summary>
    /// Gets or sets the horizontal alignment of the content.
    /// </summary>
    [ConstructorArgument("horizontalAlignment")]
    public HorizontalAlignment? HorizontalAlignment { get; set; }

    /// <summary>
    /// Gets or sets the class names to apply to the element.
    /// </summary>
    [ConstructorArgument("classes")]
    public string? Classes { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider) {
        var result = new LucideIcon();

        // Kind: binding takes precedence
        if (KindBinding is not null) result.Bind(LucideIcon.KindProperty, KindBinding);
        else result.Kind = Kind;

        // Animation: binding takes precedence
        if (AnimationBinding is not null) result.Bind(LucideIcon.AnimationProperty, AnimationBinding);
        else result.Animation = Animation;

        if (IconSize is not null) {
            switch (IconSize) {
                case IBinding binding:
                    result.Bind(LucideIcon.IconSizeProperty, binding);
                    break;
                case IConvertible conv:
                    result.IconSize = conv.ToDouble(System.Globalization.CultureInfo.InvariantCulture);
                    break;
                default:
                    throw new InvalidOperationException($"IconSize must be of type IBinding or IConvertible. Actual type: {IconSize.GetType().FullName}");
            }
        }

        // IconForeground: binding takes precedence
        if (IconForegroundBinding is not null) result.Bind(TemplatedControl.ForegroundProperty, IconForegroundBinding);
        else if (IconForeground is not null) result.Foreground = IconForeground;

        if (VerticalAlignment is not null) result.VerticalAlignment = VerticalAlignment.Value;
        if (HorizontalAlignment is not null) result.HorizontalAlignment = HorizontalAlignment.Value;

        if (!string.IsNullOrWhiteSpace(Classes)) {
            result.Classes.AddRange(global::Avalonia.Controls.Classes.Parse(Classes!));
        }

        return result;
    }
}
