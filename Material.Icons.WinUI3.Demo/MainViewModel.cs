using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using DynamicData;
using DynamicData.Binding;

using Microsoft.UI.Xaml.Data;

namespace Material.Icons.WinUI3.Demo;

public class MainViewModel : INotifyPropertyChanged {
    private readonly SourceList<PackIconKindGroup> _kindsSource = new();
    private readonly ObservableCollectionExtended<PackIconKindGroup> _kinds = new();
    private readonly Subject<Func<PackIconKindGroup, bool>> _kindsFilterSubject = new();

    public MainViewModel() {
        var scheduler = new DispatcherScheduler(Dispatcher.CurrentDispatcher);
        _kindsSource.AddRange(
            (from name in Enum.GetNames<MaterialIconKind>()
                let kind = Enum.Parse<MaterialIconKind>(name)
                let value = (int)kind
                let item = (Name: name, Kind: kind, Value: value)
                group item by item.Value
                into g
                select new PackIconKindGroup(g.Select(x => x.Name)))
            .OrderBy(x => x.DisplayName));
        _kindsSource
            .Connect()
            .Filter(_kindsFilterSubject
                .Throttle(TimeSpan.FromMilliseconds(500)))
            .Sort(SortExpressionComparer<PackIconKindGroup>.Ascending(p => p.DisplayName))
            .ObserveOn(scheduler)
            .Bind(_kinds)
            .Subscribe();

        Kinds.Source = _kinds;

        _kindsFilterSubject.OnNext(_ => true);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public CollectionViewSource Kinds { get; set; } = new();

    public PackIconKindGroup? Group {
        get;
        set {
            field = value;
            CopyText = value is null ? null : $"<wpf:MaterialIcon Kind=\"{value.Kind}\" />";
        }
    }

    public string? SearchText {
        get;
        set {
            field = value;
            if (string.IsNullOrWhiteSpace(value)) {
                _kindsFilterSubject.OnNext(_ => true);
            }
            else {
                _kindsFilterSubject.OnNext(kindGroup =>
                    kindGroup.Names.Any(a => a.Contains(value, StringComparison.CurrentCultureIgnoreCase)));
            }
        }
    }

    public string? CopyText {
        get;
        set {
            if (value == field) return;
            field = value;
            OnPropertyChanged();
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
