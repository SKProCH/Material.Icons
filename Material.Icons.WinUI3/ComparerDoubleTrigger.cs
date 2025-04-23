using Microsoft.UI.Xaml;

namespace Material.Icons.WinUI3;

internal class ComparerDoubleTrigger : StateTriggerBase {
    public ComparerDoubleTrigger() {
        UpdateTrigger();
    }
    
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(double), typeof(ComparerDoubleTrigger), 
        new PropertyMetadata(null, OnValueChanged));

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        var trigger = (ComparerDoubleTrigger)d;
        trigger.UpdateTrigger();
    }

    public double Value {
        get { return (double)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }

    public bool IsEqual { get; set; } = true;
    
    private void UpdateTrigger() {
        var isNan = double.IsNaN(Value as double? ?? 0);
        SetActive(IsEqual ? isNan : !isNan);
    }
}
