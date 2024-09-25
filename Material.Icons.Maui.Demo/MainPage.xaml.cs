using ReactiveUI;

namespace Material.Icons.Maui.Demo;

public partial class MainPage {
    public MainPage(MainViewModel viewModel) {
        Controls.Init();

        BindingContext = viewModel;

        InitializeComponent();

        this.WhenActivated(d => {
            this.BindCommand(
                ViewModel,
                vm => vm.ShowDetails,
                v => v.ShowDetails,
                vm => vm.Group);
        });
    }
}
