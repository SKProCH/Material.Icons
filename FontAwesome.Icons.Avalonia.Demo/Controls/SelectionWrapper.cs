using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;

namespace FontAwesome.Icons.Avalonia.Demo.Controls {
    [PseudoClasses("selectednow")]
    public class SelectionWrapper : ContentControl {
        public static readonly StyledProperty<object> DataSourceProperty = AvaloniaProperty.Register<SelectionWrapper, object>(nameof(DataSource));

        public static readonly StyledProperty<object> CurrentSelectedProperty = AvaloniaProperty.Register<SelectionWrapper, object>(nameof(CurrentSelected));

        public static readonly DirectProperty<SelectionWrapper, bool> SelectedNowProperty = AvaloniaProperty.RegisterDirect<SelectionWrapper, bool>(
            nameof(SelectedNow),
            wrapper => wrapper.CurrentSelected == wrapper.DataSource);

        static SelectionWrapper() {
            PointerPressedEvent.Raised.Subscribe(tuple => {
                if (tuple.Item1 is SelectionWrapper selectionWrapper) {
                    selectionWrapper.CurrentSelected = selectionWrapper.DataSource;
                }
            });

            CurrentSelectedProperty.Changed.Subscribe(args => {
                if (args.Sender is SelectionWrapper selectionWrapper) {
                    selectionWrapper.UpdateSelectedNow();
                }
            });

            DataSourceProperty.Changed.Subscribe(args => {
                if (args.Sender is SelectionWrapper selectionWrapper) selectionWrapper.UpdateSelectedNow();
            });

            SelectedNowProperty.Changed.Subscribe(args => {
                if (args.Sender is SelectionWrapper selectionWrapper) {
                    if (args.NewValue.Value) {
                        selectionWrapper.PseudoClasses.Add(":selectednow");
                    }
                    else {
                        selectionWrapper.PseudoClasses.Remove(":selectednow");
                    }
                }
            });
        }

        public object DataSource {
            get => GetValue(DataSourceProperty);
            set => SetValue(DataSourceProperty, value);
        }

        public object CurrentSelected {
            get => GetValue(CurrentSelectedProperty);
            set => SetValue(CurrentSelectedProperty, value);
        }

        public bool SelectedNow {
            get;
            private set => SetAndRaise(SelectedNowProperty, ref field, value);
        }

        public void UpdateSelectedNow() {
            SelectedNow = DataSource != null && DataSource == CurrentSelected;
        }
    }
}
