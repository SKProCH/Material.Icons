using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Material.Icons.WPF {
    internal sealed class SpacingAsMarginConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            var spacing = (double)values[0];
            var orientation = (Orientation)values[1];
            return orientation == Orientation.Horizontal
                ? new Thickness(spacing, 0, 0, 0)
                : new Thickness(0, spacing, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
