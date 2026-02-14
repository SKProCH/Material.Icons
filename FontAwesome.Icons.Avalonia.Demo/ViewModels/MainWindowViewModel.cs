using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData.Binding;
using FontAwesome.Icons.Avalonia.Demo.Models;
using ReactiveUI;

namespace FontAwesome.Icons.Avalonia.Demo.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        private readonly PackIconKindGroup[] _packIconKinds;
        private PackIconKindGroup[] _kinds;

        public int IconCount => _packIconKinds.Length;

        public PackIconKindGroup[] Kinds {
            get => _kinds;
            set => this.RaiseAndSetIfChanged(ref _kinds, value);
        }

        public PackIconKindGroup? Group {
            get;
            set => this.RaiseAndSetIfChanged(ref field, value);
        }

        public string? SearchText {
            get;
            set => this.RaiseAndSetIfChanged(ref field, value);
        }

        public IEnumerable<FontAwesomeIconAnimation> Animations { get; set; } = Enum.GetValues<FontAwesomeIconAnimation>();

        public FontAwesomeIconKind RandomIconKind {
            get;
            set => this.RaiseAndSetIfChanged(ref field, value);
        }

        public FontAwesomeIconAnimation Animation {
            get;
            set => this.RaiseAndSetIfChanged(ref field, value);
        } = FontAwesomeIconAnimation.None;

        public string DisabledIconText => "Disabled";

        public MainWindowViewModel() {
            _packIconKinds = Enum.GetNames<FontAwesomeIconKind>()
                    .GroupBy(Enum.Parse<FontAwesomeIconKind>)
                    .Select(g => new PackIconKindGroup(g))
                    .OrderBy(x => x.Kind)
                    .ToArray();

            _kinds = _packIconKinds;

            this.WhenValueChanged(model => model.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .Subscribe(DoSearch);
        }

        private void DoSearch(string? text) {
            text = text?.Trim();
            if (string.IsNullOrWhiteSpace(text))
                // Reset to all icons
                Kinds = _packIconKinds;
            else {
                // Search for given icon
                Kinds = _packIconKinds.Where(x =>
                        x.Aliases.Any(a => a.Contains(text, StringComparison.CurrentCultureIgnoreCase)))
                        .ToArray();
            }
        }

        public void Unselect() {
            Group = null;
        }

        public async Task CopySelected() {
            if (Group is null) return;
            await App.Clipboard.SetTextAsync($"<icon:FontAwesomeIcon Kind=\"{Group.Kind}\" />");
        }
    }
}
