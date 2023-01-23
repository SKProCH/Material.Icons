using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Material.Icons.WPF {
    public class MaterialIcon : Control {
        static MaterialIcon() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaterialIcon), new FrameworkPropertyMetadata(typeof(MaterialIcon)));
        }

        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(MaterialIconKind), typeof(MaterialIcon),
                new PropertyMetadata(default(MaterialIconKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject,
                                                        DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
            => ((MaterialIcon) dependencyObject).UpdateData();

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public MaterialIconKind Kind {
            get => (MaterialIconKind) GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        private static readonly DependencyPropertyKey DataPropertyKey
            = DependencyProperty.RegisterReadOnly(nameof(Data), typeof(string), typeof(MaterialIcon), new PropertyMetadata(""));

        // ReSharper disable once StaticMemberInGenericType
        public static readonly DependencyProperty DataProperty = DataPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary>
        [TypeConverter(typeof(GeometryConverter))]
        public string? Data {
            get => (string?) GetValue(DataProperty);
            private set => SetValue(DataPropertyKey, value);
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            UpdateData();
        }

        private void UpdateData() {
            Data = MaterialIconDataProvider.GetData(Kind);
        }
    }
}
