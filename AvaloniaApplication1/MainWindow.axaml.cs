using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using FontAwesome.Icons.Avalonia;
using FontAwesome.Icons;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            LoadIconsAsync();
        }

        private async Task LoadIconsAsync()
        {

            var allIcons = Enum.GetValues(typeof(FontAwesome.Icons.FontAwesomeIconKind)).Cast<FontAwesomeIconKind>();
            foreach (var icon in allIcons)
            {
                // Ensure UI update happens on the UI thread
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    faIcon.Kind = icon;
                });

                // Wait for 1 second
                await Task.Delay(200);
            }
        }
    }
}