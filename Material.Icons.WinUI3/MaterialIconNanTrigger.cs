using Microsoft.UI.Xaml;

namespace Material.Icons.WinUI3;

/// <summary>
/// Since UWP is a piece of crap, and don't have any proper ways to conditionally stylize controls
/// like styles in avalonia or triggers in WPF, we need this bullshit
/// </summary>
internal class MaterialIconNanTrigger : StateTriggerBase {
    public MaterialIconNanTrigger() {
        UpdateTrigger();
    }
    
    public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
        nameof(Width), typeof(double), typeof(MaterialIconNanTrigger), 
        new PropertyMetadata(double.NaN, OnValueChanged));

    public double Width {
        get { return (double)GetValue(WidthProperty); }
        set { SetValue(WidthProperty, value); }
    }

    public static readonly DependencyProperty HeightProperty = DependencyProperty.Register(
        nameof(Height), typeof(double), typeof(MaterialIconNanTrigger), 
        new PropertyMetadata(double.NaN, OnValueChanged));

    public double Height {
        get { return (double)GetValue(HeightProperty); }
        set { SetValue(HeightProperty, value); }
    }

    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register(
        nameof(IconSize), typeof(double), typeof(MaterialIconNanTrigger), 
        new PropertyMetadata(double.NaN, OnValueChanged));

    public double IconSize {
        get { return (double)GetValue(IconSizeProperty); }
        set { SetValue(IconSizeProperty, value); }
    }
    
    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        var trigger = (MaterialIconNanTrigger)d;
        trigger.UpdateTrigger();
    }
    
    private void UpdateTrigger() {
        var allNonNan = double.IsNaN(Width)
                        && double.IsNaN(Height)
                        && double.IsNaN(IconSize);
        SetActive(allNonNan);
    }
}
