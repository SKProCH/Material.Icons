using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

namespace Lucide.Icons.Avalonia;

public class LucideIconTextExt : LucideIconExt {
    public LucideIconTextExt() { }

    public LucideIconTextExt(LucideIconKind kind) : base(kind)
    {
    }

    public LucideIconTextExt(LucideIconKind kind, LucideIconAnimation animation) : base(kind, animation)
    {
    }

    public LucideIconTextExt(LucideIconKind kind, double iconSize, LucideIconAnimation animation = LucideIconAnimation.None) : base(kind, iconSize, animation)
    {
    }

    public LucideIconTextExt(LucideIconKind kind, string text, LucideIconAnimation animation = LucideIconAnimation.None)
        : base(kind, animation)
    {
        Text = text;
    }

    public LucideIconTextExt(LucideIconKind kind, double iconSize, string text, LucideIconAnimation animation = LucideIconAnimation.None)
        : base(kind, iconSize, animation)
    {
        Text = text;
    }

    public LucideIconTextExt(LucideIconKind kind, double iconSize, Dock iconPlacement, string text, LucideIconAnimation animation = LucideIconAnimation.None)
        : base(kind, iconSize, animation) {
        IconPlacement = iconPlacement;
        Text = text;
    }

    [ConstructorArgument("iconPlacement")]
    public Dock? IconPlacement { get; set; }

    [ConstructorArgument("spacing")]
    public double? Spacing { get; set; }

    [ConstructorArgument("text")]
    public object? Text { get; set; }

    [ConstructorArgument("isTextSelectable")]
    public bool? IsTextSelectable { get; set; }

    [ConstructorArgument("verticalContentAlignment")]
    public VerticalAlignment? VerticalContentAlignment { get; set; }

    [ConstructorArgument("horizontalContentAlignment")]
    public HorizontalAlignment? HorizontalContentAlignment { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider) {
        // If no text is provided and it's not a binding, fall back to base
        if (Text is null || (Text is not IBinding && Text is string textString && string.IsNullOrWhiteSpace(textString)))
            return base.ProvideValue(serviceProvider);

        var result = new LucideIconText();

        // Kind: binding takes precedence
        if (KindBinding is not null) result.Bind(LucideIcon.KindProperty, KindBinding);
        else result.Kind = Kind;

        // Animation: binding takes precedence
        if (AnimationBinding is not null) result.Bind(LucideIcon.AnimationProperty, AnimationBinding);
        else result.Animation = Animation;

        // Apply Text (supports binding or direct value)
        if (Text is IBinding textBinding) result.Bind(LucideIconText.TextProperty, textBinding);
        else if (Text is string textValue) result.Text = textValue;

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
