using System.Reactive.Linq;

using DynamicData.Binding;
using DynamicData;

using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Material.Icons.Maui.Demo;

public partial class MainViewModel : ReactiveObject {
    private readonly SourceList<PackIconKindGroup> _kindsSource = new();

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
    }

    private static Func<PackIconKindGroup, bool> CreateUnfilteredFilter() {
        return _ => true;
    }
    
    private static Func<PackIconKindGroup, bool> CreateTextFilter(string searchText) {
        return kindGroup => kindGroup.Names.Any(a => a.Contains(searchText, StringComparison.CurrentCultureIgnoreCase));
    }
}
