using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace Material.Icons.Avalonia {
    public class MaterialIconStyles : AvaloniaObject, IStyle, IResourceProvider {
        private readonly ResourceInclude _materialIconResources = new(default(Uri?)) { Source = new Uri("avares://Material.Icons.Avalonia/MaterialIcon.axaml") };
        /// <inheritdoc />
        public bool TryGetResource(object key, ThemeVariant? theme, out object? value) {
            return _materialIconResources.Loaded.TryGetResource(key, theme, out value);
        }

        /// <inheritdoc />
        public bool HasResources => true;

        /// <inheritdoc />
        public SelectorMatchResult TryAttach(IStyleable target, object? host) {
            return SelectorMatchResult.NeverThisType;
        }

        /// <inheritdoc />
        public IReadOnlyList<IStyle> Children { get; } = new List<IStyle>();

        /// <inheritdoc />
        public void AddOwner(IResourceHost owner) => _materialIconResources.Loaded.AddOwner(owner);

        /// <inheritdoc />
        public void RemoveOwner(IResourceHost owner) => _materialIconResources.Loaded.RemoveOwner(owner);

        /// <inheritdoc />
        public IResourceHost? Owner => _materialIconResources.Loaded.Owner;

        /// <inheritdoc />
        public event EventHandler? OwnerChanged {
            add {
                if (_materialIconResources.Loaded is IResourceProvider rp) {
                    rp.OwnerChanged += value;
                }
            }
            remove {
                if (_materialIconResources.Loaded is IResourceProvider rp) {
                    rp.OwnerChanged -= value;
                }
            }
        }
    }
}
