using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Material.Icons.Avalonia {
    public class MaterialIcon : TemplatedControl {
        private static readonly Lazy<IDictionary<MaterialIconKind, string>> _dataIndex = new(MaterialIconDataFactory.DataSetCreate);

        static MaterialIcon() {
            KindProperty.Changed.Subscribe(args => (args.Sender as MaterialIcon)?.UpdateData());
        }

        public static readonly AvaloniaProperty<MaterialIconKind> KindProperty = AvaloniaProperty.Register<MaterialIcon, MaterialIconKind>(nameof(Kind));

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public MaterialIconKind Kind {
            get => GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        private static readonly AvaloniaProperty<string?>
            DataProperty = AvaloniaProperty.RegisterDirect<MaterialIcon, string?>(nameof(Data), icon => icon.Data);

        private string? _data;

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary>
        [TypeConverter(typeof(GeometryConverter))]
        public string? Data {
            get => _data;
            private set => SetAndRaise(DataProperty, ref _data, value);
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e) {
            base.OnTemplateApplied(e);
            UpdateData();
        }

        private void UpdateData() {
            string? data = null;
            _dataIndex.Value?.TryGetValue(Kind, out data);
            Data = data;
        }
    }

    public class GeometryConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return Geometry.Parse(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}