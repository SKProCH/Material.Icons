using Microsoft.UI.Xaml;

namespace Material.Icons.WinUI3;

/// <summary>
/// Since UWP is a piece of crap, and don't have any proper ways to conditionally stylize controls
/// like styles in avalonia or triggers in WPF, we need this bullshit
/// </summary>
internal class MaterialIconNonNanTrigger : StateTriggerBase {
    public MaterialIconNonNanTrigger() {
        UpdateTrigger();
    }
    
    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register(
        nameof(IconSize), typeof(double), typeof(MaterialIconNonNanTrigger), 
        new PropertyMetadata(double.NaN, OnValueChanged));

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        var trigger = (MaterialIconNonNanTrigger)d;
        trigger.UpdateTrigger();
    }

    public double IconSize {
        get { return (double)GetValue(IconSizeProperty); }
        set { SetValue(IconSizeProperty, value); }
    }
    
    private void UpdateTrigger() {
        var isNan = double.IsNaN(IconSize);
        SetActive(!isNan);
    }
}
