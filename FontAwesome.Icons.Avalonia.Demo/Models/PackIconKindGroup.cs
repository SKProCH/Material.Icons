using System;
using System.Collections.Generic;
using System.Linq;

namespace FontAwesome.Icons.Avalonia.Demo.Models {
    public class PackIconKindGroup {
        public PackIconKindGroup(IEnumerable<string> kinds) {
            if (kinds is null) throw new ArgumentNullException(nameof(kinds));

            var allValues = kinds as string[] ?? kinds.ToArray();
            if (allValues.Length == 0) throw new ArgumentException($"{nameof(kinds)} must contain at least one value");
            Kind = allValues[0];
            Aliases = allValues
                     .OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase)
                     .ToArray();
        }

        public string Kind { get; }
        public string[] Aliases { get; }
    }
}
