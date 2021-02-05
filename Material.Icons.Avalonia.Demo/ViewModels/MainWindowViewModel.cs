using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using DynamicData.Binding;
using Material.Icons.Avalonia.Demo.Models;
using ReactiveUI;

namespace Material.Icons.Avalonia.Demo.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        private readonly Lazy<IEnumerable<PackIconKindGroup>> _packIconKinds;
        private IEnumerable<PackIconKindGroup>? _kinds;
        private PackIconKindGroup? _group;
        private string? _searchText;

        public MainWindowViewModel() {
            _packIconKinds = new Lazy<IEnumerable<PackIconKindGroup>>(() =>
                Enum.GetNames(typeof(MaterialIconKind))
                    .GroupBy(k => (MaterialIconKind) Enum.Parse(typeof(MaterialIconKind), k))
                    .Select(g => new PackIconKindGroup(g))
                    .OrderBy(x => x.Kind)
                    .ToList());

            this.WhenValueChanged(model => model.SearchText).Subscribe(DoSearch);
            CopyText = this.WhenValueChanged(model => model.Group).Where(group => group != null).Select(group => $"<avalonia:PackIcon Kind=\"{group.Kind}\"/>");
        }

        private async void DoSearch(string text) {
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

        public IObservable<string> CopyText { get; set; }
    }
}