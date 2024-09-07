// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Material.Icons.WinUI3.Demo;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow {
    public MainWindow() {
        InitializeComponent();

        Root.DataContext = new MainViewModel();
    }
}
