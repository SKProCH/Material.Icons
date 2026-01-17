using Microsoft.UI.Xaml;

namespace Material.Icons.WinUI3;

/// <summary>
/// A state trigger that activates when the icon kind is Invisible.
/// </summary>
internal class MaterialIconInvisibleTrigger : StateTriggerBase {
    public MaterialIconInvisibleTrigger() {
        UpdateTrigger();
    }

    public static readonly DependencyProperty KindProperty = DependencyProperty.Register(
        nameof(Kind), typeof(MaterialIconKind), typeof(MaterialIconInvisibleTrigger),
        new PropertyMetadata(default(MaterialIconKind), OnKindChanged));

    public MaterialIconKind Kind {
        get => (MaterialIconKind)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    private static void OnKindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        var trigger = (MaterialIconInvisibleTrigger)d;
        trigger.UpdateTrigger();
    }

    private void UpdateTrigger() {
        SetActive(Kind == MaterialIconKind.Invisible);
    }
}
