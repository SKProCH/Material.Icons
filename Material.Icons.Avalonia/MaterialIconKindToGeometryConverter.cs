using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Material.Icons.Avalonia {
    public class MaterialIconKindToGeometryConverter : IValueConverter {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
            if (value is MaterialIconKind kind) {
                return Geometry.Parse(MaterialIconDataProvider.GetData(kind));
            }

            return BindingOperations.DoNothing;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
