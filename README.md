# Material.Icons
Parsed icons set from [materialdesignicons.com](https://materialdesignicons.com/) and display control implementations for different GUI frameworks.
All information about icons is stored in text form and is automatically generated every time Material.Icons is built. Icons are graphically encoded via SVG Path.



## Meta library
#### Getting started
Install [Material.Icons nuget package](https://www.nuget.org/packages/Material.Icons/):
```
dotnet add package Material.Icons
```
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
#### Using
To display icon we can use `MaterialIcon` control.
```
<wpf:MaterialIcon Kind="Abacus"></wpf:MaterialIcon>
```
The `Foreground` property controls the color of the icon.



## Avalonia
#### Getting started
Install [Material.Icons.Avalonia nuget package](https://www.nuget.org/packages/Material.Icons.Avalonia/):
```
dotnet add package Material.Icons.Avalonia
```
Include styles in `App.xaml`
```
<Application ...>
  <Application.Styles>
    ...
    <StyleInclude Source="avares://Material.Icons.Avalonia/App.xaml"></StyleInclude>
  </Application.Styles>
</Application>
```
#### Using
Use `MaterialIcon` control:
```
<avalonia:MaterialIcon Kind="Abacus"></avalonia:MaterialIcon>
```
The `Foreground` property controls the color of the icon.
