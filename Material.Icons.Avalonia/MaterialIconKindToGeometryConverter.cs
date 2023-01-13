using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Material.Icons.Avalonia {
    public class MaterialIconKindToGeometryConverter : IValueConverter {
        private static readonly Lazy<IDictionary<MaterialIconKind, string>> _dataIndex = new(MaterialIconDataFactory.DataSetCreate);
        public virtual object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
            if (value is MaterialIconKind kind) {
                return Geometry.Parse(_dataIndex.Value[kind]);
            }

            return BindingOperations.DoNothing;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}