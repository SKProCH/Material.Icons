using System;
using Avalonia.Controls;
using Avalonia.Threading;

namespace Material.Icons.Avalonia.Demo.Views {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            var values = Enum.GetValues<MaterialIconKind>();

            DispatcherTimer.Run(() => {

                RandomIcon.Kind = values[Random.Shared.Next(0, values.Length)];
                RandomImageIcon.Kind = values[Random.Shared.Next(0, values.Length)];
#if RELEASE
                // Without this line, image will not be updated with the new icon
                RandomImage.InvalidateVisual();
#endif
                return true;
            }, TimeSpan.FromSeconds(1));
        }
    }
}
