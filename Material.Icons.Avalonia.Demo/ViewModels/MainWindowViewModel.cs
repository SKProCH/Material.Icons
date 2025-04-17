using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using DynamicData.Binding;
using Material.Icons.Avalonia.Demo.Models;
using ReactiveUI;

namespace Material.Icons.Avalonia.Demo.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        private readonly PackIconKindGroup[] _packIconKinds;
        private PackIconKindGroup[] _kinds;
        private PackIconKindGroup? _group;
        private string? _searchText;

        private readonly DispatcherTimer _searchDebounceTimer = new DispatcherTimer() {
            Interval = TimeSpan.FromMilliseconds(500)
        };

        public MainWindowViewModel() {
            _packIconKinds = Enum.GetNames(typeof(MaterialIconKind))
                    .GroupBy(k => (MaterialIconKind) Enum.Parse(typeof(MaterialIconKind), k))
                    .Select(g => new PackIconKindGroup(g))
                    .OrderBy(x => x.Kind)
                    .ToArray();

            _kinds = _packIconKinds;

            this.WhenValueChanged(model => model.SearchText).Subscribe(s => {
                _searchDebounceTimer.Stop();
                _searchDebounceTimer.Start();
            });

            _searchDebounceTimer.Tick += (s, e) => {
                DoSearch(SearchText);
            };
        }

        private void DoSearch(string? text) {
            _searchDebounceTimer.Stop();
            if (string.IsNullOrWhiteSpace(text))
                Kinds = _packIconKinds;
            else {
                Kinds = _packIconKinds.Where(x =>
                        x.Aliases.Any(a => a.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0))
                        .ToArray();
            }
        }

        public PackIconKindGroup[] Kinds {
            get => _kinds;
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
