// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Material.Icons.Avalonia.Demo.ViewModels;

#pragma warning disable 8600
#pragma warning disable 8602
#pragma warning disable 8603

namespace Material.Icons.Avalonia.Demo {
    public class ViewLocator : IDataTemplate {
        public bool SupportsRecycling => false;

        public Control Build(object? data) {
            var name = data.GetType().FullName.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null) {
                return (Control) Activator.CreateInstance(type);
            }
            else {
                return new TextBlock {Text = "Not Found: " + name};
            }
        }

        public bool Match(object? data) {
            return data is ViewModelBase;
        }
    }
}
