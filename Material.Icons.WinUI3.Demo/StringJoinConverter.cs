using System;
using System.Collections;
using System.Linq;

using Microsoft.UI.Xaml.Data;

namespace Material.Icons.WinUI3.Demo
{
    public partial class StringJoinConverter : IValueConverter
    {
        public string? Separator { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language) {
            IEnumerable values = value as IEnumerable ?? Array.Empty<object>();
            return string.Join(Separator ?? "", values.OfType<object>());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotSupportedException();
        }
    }
}
