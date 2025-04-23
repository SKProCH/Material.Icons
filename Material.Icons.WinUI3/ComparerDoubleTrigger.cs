using Microsoft.UI.Xaml;

namespace Material.Icons.WinUI3;

internal class ComparerDoubleTrigger : StateTriggerBase {
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(object), typeof(ComparerDoubleTrigger), 
        new PropertyMetadata(null, OnValueChanged));

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        var trigger = (ComparerDoubleTrigger)d;
        trigger.UpdateTrigger();
    }

    public object? Value {
        get { return (object?)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }
    
    public bool IsEqual { get; set; }
    
    private void UpdateTrigger() {
        SetActive(double.IsNaN(Value as double? ?? 0));
    }
}
