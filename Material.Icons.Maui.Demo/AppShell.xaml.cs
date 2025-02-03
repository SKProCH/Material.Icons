namespace Material.Icons.Maui.Demo;

public partial class AppShell {
    public AppShell() {
        InitializeComponent();

        Routing.RegisterRoute("Main/Details", typeof(DetailsPage));
    }
}
