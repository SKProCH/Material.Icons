using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace Material.Icons.WPF.Demo {
    public class MainViewModel : INotifyPropertyChanged {
        private Lazy<IEnumerable<PackIconKindGroup>> _packIconKinds;

        public MainViewModel() {
            Kinds.Filter += OnFilter;
            Kinds.Source = Enum.GetNames(typeof(MaterialIconKind))
                               .GroupBy(k => (MaterialIconKind) Enum.Parse(typeof(MaterialIconKind), k))
                               .Select(g => new PackIconKindGroup(g))
                               .OrderBy(x => x.Kind)
                               .ToList();
        }

        private void OnFilter(object sender, FilterEventArgs e) {
            if (string.IsNullOrWhiteSpace(SearchText))
                e.Accepted = true;
            else {
                if (e.Item is PackIconKindGroup kindGroup)
                    e.Accepted = kindGroup.Aliases.Any(a => a.IndexOf(SearchText, StringComparison.CurrentCultureIgnoreCase) >= 0);
            }
        }

        public CollectionViewSource Kinds { get; set; } = new CollectionViewSource();

        public PackIconKindGroup Group {
            get;
            set {
                field = value;
                CopyText = $"<wpf:MaterialIcon Kind=\"{value.Kind}\" />";
            }
        }

        public string SearchText {
            get;
            set {
                field = value;
                Kinds.View.Refresh();
            }
        }

        public string CopyText {
            get;
            set {
                if (value == field) return;
                field = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}