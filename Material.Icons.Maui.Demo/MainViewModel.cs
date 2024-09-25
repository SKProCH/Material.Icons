using System.Reactive;
using System.Reactive.Linq;

using DynamicData.Binding;
using DynamicData;

using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Material.Icons.Maui.Demo;

public partial class MainViewModel : ReactiveObject {
    [Reactive]
    private SourceList<PackIconKindGroup> _kindsSource = new();

    [Reactive]
    private ObservableCollectionExtended<PackIconKindGroup> _kinds = new();
    
    [Reactive]
    private string? _searchText;

    [Reactive]
    private PackIconKindGroup? _group;
    
    [ObservableAsProperty]
    private string? _copyText;

    public MainViewModel() {
        _kindsSource.AddRange(
            (from name in Enum.GetNames<MaterialIconKind>()
                let kind = Enum.Parse<MaterialIconKind>(name)
                let value = (int)kind
                let item = (Name: name, Kind: kind, Value: value)
                group item by item.Value
                into g
                select new PackIconKindGroup(g.Select(x => x.Name)))
            .OrderBy(x => x.DisplayName));

        Group = _kindsSource.Items.FirstOrDefault();

        var kindsFilter = this.WhenAnyValue(x => x.SearchText)
            .Select(text => string.IsNullOrEmpty(text)
                ? CreateUnfilteredFilter()
                : CreateTextFilter(text))
            .Throttle(TimeSpan.FromMilliseconds(250));

        _kindsSource
            .Connect()
            .Filter(kindsFilter)
            .Sort(SortExpressionComparer<PackIconKindGroup>.Ascending(p => p.DisplayName))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(_kinds)
            .Subscribe();

        _copyTextHelper = this.WhenAnyValue(x => x.Group)
            .Select(value => value is null ? null : $"<wpf:MaterialIcon Kind=\"{value.Kind}\" />")
            .ToProperty(this, x => x.CopyText);

        DetailsViewModel = new DetailsViewModel() {
            Icons = _kinds,
            SelectedIcon = Group,
        };

        DetailsViewModel.WhenAnyValue(x => x.SelectedIcon)
            .Subscribe(item => Group = item);

        var canShowDetails = this.WhenAnyValue(x => x.Group)
            .Select(value => value is not null);
        ShowDetails = ReactiveCommand.CreateFromTask<PackIconKindGroup>(ShowDetailsAsync, canShowDetails);
    }

    public DetailsViewModel DetailsViewModel { get; }

    public ReactiveCommand<PackIconKindGroup, Unit> ShowDetails { get; }

    private static Func<PackIconKindGroup, bool> CreateUnfilteredFilter() {
        return _ => true;
    }
    
    private static Func<PackIconKindGroup, bool> CreateTextFilter(string searchText) {
        return kindGroup => kindGroup.Names.Any(a => a.Contains(searchText, StringComparison.CurrentCultureIgnoreCase));
    }

    private async Task ShowDetailsAsync(PackIconKindGroup icon, CancellationToken cancellationToken) {
        DetailsViewModel.SelectedIcon = icon;
        await Shell.Current.GoToAsync("Details", new Dictionary<string, object> {
            { "ViewModel", DetailsViewModel },
        });
    }
}
