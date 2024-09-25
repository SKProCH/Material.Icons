using DynamicData.Binding;

using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Material.Icons.Maui.Demo;

public partial class DetailsViewModel : ReactiveObject {
    [Reactive]
    private PackIconKindGroup? _selectedIcon;

    [Reactive]
    private ObservableCollectionExtended<PackIconKindGroup> _icons = [];
}
