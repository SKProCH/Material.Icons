namespace Material.Icons.Maui.Demo;

public partial class MainPage {
    public MainPage(MainViewModel viewModel) {
        Controls.Init();

        BindingContext = viewModel;

        InitializeComponent();
    }
}
