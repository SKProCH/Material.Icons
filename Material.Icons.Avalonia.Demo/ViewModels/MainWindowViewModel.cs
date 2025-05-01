using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Media;
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
        private MaterialIconAnimation _animation = MaterialIconAnimation.None;

        public int IconCount => _packIconKinds.Length;

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

        public IEnumerable<MaterialIconAnimation> Animations { get; set; } = Enum.GetValues<MaterialIconAnimation>();

        public MaterialIconAnimation Animation {
            get => _animation;
            set => this.RaiseAndSetIfChanged(ref _animation, value);
        }

        public MainWindowViewModel() {
            _packIconKinds = Enum.GetNames<MaterialIconKind>()
                    .GroupBy(Enum.Parse<MaterialIconKind>)
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
            else if (text == "$cache") {
                // Test cache efficiency
                Dispatcher.UIThread.Post(() => {
                    var converter = new MaterialIconKindToGeometryConverter();

                    // Cache test icon
                    MaterialIconOptions.UseCache = true;
                    var geometry = (Geometry)converter.Convert(MaterialIconKind.Abacus, typeof(MaterialIconKind), null, CultureInfo.CurrentCulture);

                    foreach (var testCount in new[] { 10, 100, 1_000, 10_000, 50_000 }) {
                        foreach (var cache in new[] { false, true }) {
                            MaterialIconOptions.UseCache = cache;

                            var sw = Stopwatch.StartNew();

                            for (var i = 0; i < testCount; i++) {
                                converter.Convert(MaterialIconKind.Abacus, typeof(MaterialIconKind), null,
                                    CultureInfo.CurrentCulture);
                            }

                            sw.Stop();
                            Trace.WriteLine($"Cache={cache.ToString(),-5} for {testCount.ToString(),-5} icons, results in {sw.ElapsedTicks} ticks / {sw.ElapsedMilliseconds}ms, Memory: {88 * (cache ? 1 : testCount):N0} bytes");
                        }
                        Trace.WriteLine(string.Empty);
                    }
                });
            }
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
            await App.Clipboard.SetTextAsync($"<icon:MaterialIcon Kind=\"{Group.Kind}\" />");
        }
    }
}
