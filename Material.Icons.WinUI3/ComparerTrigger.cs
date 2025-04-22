using Microsoft.UI.Xaml;

namespace Material.Icons.WinUI3;

internal class ComparerTrigger : StateTriggerBase {
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(object), typeof(ComparerTrigger), new PropertyMetadata(null));

    public object? Value {
        get { return (object?)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }
    
    public object? ExpectedValue { get; set; }
    
    public bool IsEqual { get; set; }

    private void UpdateTrigger() {
        SetActive(IsEqual ? Value == ExpectedValue : Value != ExpectedValue);
    }
}
