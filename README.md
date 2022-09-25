[icons-nuget]: https://www.nuget.org/packages/Material.Icons/
[wpf-nuget]: https://www.nuget.org/packages/Material.Icons.WPF/
[avalonia-nuget]: https://www.nuget.org/packages/Material.Icons.Avalonia/

# Material.Icons
Parsed icons set from [materialdesignicons.com](https://materialdesignicons.com/) and display control implementations for different GUI frameworks.
All information about icons is stored in text form and is automatically generated every time Material.Icons is built. Icons are graphically encoded via SVG Path.



## Meta library
#### Getting started
Install [Material.Icons nuget package](https://www.nuget.org/packages/Material.Icons/):
```
dotnet add package Material.Icons
```
[![icons-nuget](https://img.shields.io/nuget/v/Material.Icons?label=Material.Icons&style=flat-square)][icons-nuget]
[![icons-nuget](https://img.shields.io/nuget/dt/Material.Icons?color=blue&label=Downloads&style=flat-square)][icons-nuget]
#### Using
Icon types stored in `MaterialIconKind` enum.  
We can access icon paths by using `MaterialIconDataFactory.DataSetCreate()`.  
We can access icons meta info by using `MaterialIconDataFactory.InstanceSetCreate()`.  



## WPF
#### Getting started
Install [Material.Icons.WPF nuget package](https://www.nuget.org/packages/Material.Icons.WPF/):
```
dotnet add package Material.Icons.WPF
```
[![wpf-nuget](https://img.shields.io/nuget/v/Material.Icons.WPF?label=Material.Icons.WPF&style=flat-square)][wpf-nuget]
[![wpf-nuget](https://img.shields.io/nuget/dt/Material.Icons.WPF?color=blue&label=Downloads&style=flat-square)][wpf-nuget]
#### Using
Add `Material.Icons.WPF` namespace to root element of your file (your IDE can suggest it or do it automatically):
```
xmlns:materialIcons="clr-namespace:Material.Icons.WPF;assembly=Material.Icons.WPF"
```
Use `MaterialIcon` control:
```xaml
<materialIcons:MaterialIcon Kind="Abacus" />
```
The `Foreground` property controls the color of the icon.



## Material.Icons.Avalonia
#### Getting started
1. Install [Material.Icons.Avalonia nuget package](https://www.nuget.org/packages/Material.Icons.Avalonia/):
    ```
    dotnet add package Material.Icons.Avalonia
    ```
   [![avalonia-nuget](https://img.shields.io/nuget/v/Material.Icons.Avalonia?label=Material.Icons.Avalonia&style=flat-square)][avalonia-nuget]
   [![avalonia-nuget](https://img.shields.io/nuget/dt/Material.Icons.Avalonia?color=blue&label=Downloads&style=flat-square)][avalonia-nuget]
2. Include styles in `App.xaml`
    ```xaml
    <Application ...>
      <Application.Styles>
        ...
        <StyleInclude Source="avares://Material.Icons.Avalonia/App.xaml" />
      </Application.Styles>
    </Application>
    ```
#### Using
Add `Material.Icons.Avalonia` namespace to root element of your file (your IDE can suggest it or do it automatically):
```
xmlns:avalonia="clr-namespace:Material.Icons.Avalonia;assembly=Material.Icons.Avalonia"
```
Use `MaterialIcon` control:
```xaml
<materialIcons:MaterialIcon Kind="Abacus" />
```
The `Foreground` property controls the color of the icon.
