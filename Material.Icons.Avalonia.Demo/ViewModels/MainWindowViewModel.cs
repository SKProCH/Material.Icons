using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using DynamicData.Binding;
using Material.Icons.Avalonia.Demo.Models;
using ReactiveUI;

namespace Material.Icons.Avalonia.Demo.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        private readonly Lazy<IEnumerable<PackIconKindGroup>> _packIconKinds;
        private IEnumerable<PackIconKindGroup>? _kinds;
        private PackIconKindGroup? _group;
        private string? _searchText;

        private readonly DispatcherTimer _searchDebounceTimer = new DispatcherTimer() {
            Interval = TimeSpan.FromMilliseconds(500)
        };

        public MainWindowViewModel() {
            _packIconKinds = new Lazy<IEnumerable<PackIconKindGroup>>(() =>
                Enum.GetNames(typeof(MaterialIconKind))
                    .GroupBy(k => (MaterialIconKind) Enum.Parse(typeof(MaterialIconKind), k))
                    .Select(g => new PackIconKindGroup(g))
                    .OrderBy(x => x.Kind)
                    .ToList());

            this.WhenValueChanged(model => model.SearchText).Subscribe(s => {
                _searchDebounceTimer.Stop();
                _searchDebounceTimer.Start();
            });

            _searchDebounceTimer.Tick += async (s, e) => {
                await DoSearch(SearchText);
            };
        }

        private async Task DoSearch(string? text) {
            _searchDebounceTimer.Stop();
            if (string.IsNullOrWhiteSpace(text))
                Kinds = _packIconKinds.Value;
            else {
                Kinds = new List<PackIconKindGroup>();
                Kinds = await Task.Run(() =>
                    _packIconKinds.Value
                                  .Where(x => x.Aliases.Any(a => a.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0))
                                  .ToList());
            }
        }

        public IEnumerable<PackIconKindGroup> Kinds {
            get => _kinds ?? _packIconKinds.Value;
            set => this.RaiseAndSetIfChanged(ref _kinds, value);
        }

        public PackIconKindGroup? Group {
            get => _group;
            set => this.RaiseAndSetIfChanged(ref _group, value);
        }

        public string? SearchText {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }
    }
}
