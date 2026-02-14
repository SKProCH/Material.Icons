using System;
using Avalonia.Controls;
using Avalonia.Threading;
using FontAwesome.Icons.Avalonia.Demo.ViewModels;

namespace FontAwesome.Icons.Avalonia.Demo.Views {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            var values = Enum.GetValues<FontAwesomeIconKind>();

            DispatcherTimer.Run(() => {
                if (DataContext is MainWindowViewModel dataContext) {
                    dataContext.RandomIconKind = values[Random.Shared.Next(0, values.Length)];
                    RandomImageIcon.Kind = dataContext.RandomIconKind;

                    // Without this line, image will not be updated with the new icon
                    RandomImage.InvalidateVisual();

                    return true;
                }

                return false;
            }, TimeSpan.FromSeconds(1));
        }
    }
}
