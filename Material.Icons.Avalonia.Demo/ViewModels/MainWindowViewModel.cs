using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;

namespace Material.Icons.Avalonia.Demo.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        private readonly Lazy<IEnumerable<PackIconKindGroup>> _packIconKinds;
        private IEnumerable<PackIconKindGroup> _kinds;

        public MainWindowViewModel() {
            _packIconKinds = new Lazy<IEnumerable<PackIconKindGroup>>(() =>
                Enum.GetNames(typeof(MaterialIconKind))
                    .GroupBy(k => (MaterialIconKind) Enum.Parse(typeof(MaterialIconKind), k))
                    .Select(g => new PackIconKindGroup(g))
                    .OrderBy(x => x.Kind)
                    .ToList());

            SearchText.Subscribe(DoSearch);
        }

        private async void DoSearch(string obj) {
            var text = obj as string;
            if (string.IsNullOrWhiteSpace(text))
                Kinds = _packIconKinds.Value;
            else {
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

        public PackIconKindGroup Group { get; set; }

        public Subject<string> SearchText { get; set; } = new Subject<string>();
    }
}