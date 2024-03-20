[icons-nuget]: https://www.nuget.org/packages/Material.Icons/
[wpf-nuget]: https://www.nuget.org/packages/Material.Icons.WPF/
[avalonia-nuget]: https://www.nuget.org/packages/Material.Icons.Avalonia/
[uno]: https://github.com/CastelloBrancoTecnologia/Material.Icons.UNO/

# Material.Icons
Parsed icons set from [materialdesignicons.com](https://materialdesignicons.com/) and display control implementations for different GUI frameworks.  
- All icons are **always up-to-date** because automatically updated every 6 hours.
- **Small package size** because icons are graphically encoded via SVG Path.
- Icon types are **strongly typed** enum, so your **IDE will suggest available variants**:  
![895428ad-6010-4ffd-bd88-61aecd50f5e1](https://user-images.githubusercontent.com/29896317/213889827-ca4f7673-115a-433e-9fde-305d55d36772.gif)

## Structure
This project consists of 3 parts:
- [![](https://img.shields.io/nuget/dt/Material.Icons?label=Material.Icons&style=flat-square)](#meta) contains info about the icons
- [![](https://img.shields.io/nuget/dt/Material.Icons.Avalonia?color=teal&label=Material.Icons.Avalonia&style=flat-square)](#avalonia) contains controls for **AvaloniaUI**
- [![](https://img.shields.io/nuget/dt/Material.Icons.WPF?color=teal&label=Material.Icons.WPF&style=flat-square)](#wpf) contains controls for **WPF**


- [FAQ](#faq) - frequently asked questions

### Community maintained
- [![](https://img.shields.io/nuget/dt/Material.Icons.UNO?color=blue&label=Material.Icons.UNO&style=flat-square)][uno] contains controls for **WinUI/UNO** (separate [repository][uno])

## Avalonia
#### Getting started
1. Install [Material.Icons.Avalonia nuget package](https://www.nuget.org/packages/Material.Icons.Avalonia/):
    ```shell
    dotnet add package Material.Icons.Avalonia
    ```
   [![avalonia-nuget](https://img.shields.io/nuget/v/Material.Icons.Avalonia?label=Material.Icons.Avalonia&style=flat-square)][avalonia-nuget]
   [![avalonia-nuget](https://img.shields.io/nuget/dt/Material.Icons.Avalonia?color=blue&label=Downloads&style=flat-square)][avalonia-nuget]
2. Include styles in `App.xaml` (for `2.0.0` version and higher):
    ```xaml
    <Application xmlns:materialIcons="clr-namespace:Material.Icons.Avalonia;assembly=Material.Icons.Avalonia" 
                 ...>
      <Application.Styles>
        ...
        <materialIcons:MaterialIconStyles />
      </Application.Styles>
    </Application>
    ```
#### Using
Add `Material.Icons.Avalonia` namespace to the root element of your file (your IDE can suggest it or do it automatically):
```
xmlns:materialIcons="clr-namespace:Material.Icons.Avalonia;assembly=Material.Icons.Avalonia"
```
Use `MaterialIcon` control:
```xaml
<materialIcons:MaterialIcon Kind="Abacus" />
```
The `Foreground` property controls the color of the icon.  
Also, there is `MaterialIconExt` which allows you to use is as the markup extension:
```xaml
<Button Content="{materialIcons:MaterialIconExt Kind=Abacus}" />
```

## Avalonia FuncUI (F#)
#### Getting started
1. Install [Material.Icons.Avalonia nuget package](https://www.nuget.org/packages/Material.Icons.Avalonia/):
    ```shell
    dotnet add package Material.Icons.Avalonia
    ```
   [![avalonia-nuget](https://img.shields.io/nuget/v/Material.Icons.Avalonia?label=Material.Icons.Avalonia&style=flat-square)][avalonia-nuget]
   [![avalonia-nuget](https://img.shields.io/nuget/dt/Material.Icons.Avalonia?color=blue&label=Downloads&style=flat-square)][avalonia-nuget]
2. Import styles in Application (or if you use XAML check instructions for plain Avalonia)
    ```fsharp
    type App() =
        inherit Application()
    
        override this.Initialize() =
            ..
            this.Styles.Add(MaterialIconStyles(null))
            ..
    ```
3. Create bindings for `MaterialIcon`
    ```fsharp
    namespace Avalonia.FuncUI.DSL
    
    [<AutoOpen>]
    module MaterialIcon =
        open Material.Icons
        open Material.Icons.Avalonia
        open Avalonia.FuncUI.Types
        open Avalonia.FuncUI.Builder
    
        let create (attrs: IAttr<MaterialIcon> list): IView<MaterialIcon> =
            ViewBuilder.Create<MaterialIcon>(attrs)
    
        type MaterialIcon with
    
            static member kind<'t when 't :> MaterialIcon>(value: MaterialIconKind) : IAttr<'t> =
                AttrBuilder<'t>.CreateProperty<MaterialIconKind>(MaterialIcon.KindProperty, value, ValueNone)
    ```
4. Use
    ```fsharp
    Button.create [
         Button.content (
             MaterialIcon.create [
                 MaterialIcon.kind MaterialIconKind.Export
            ]
        )
    ]
    ```

## WPF
#### Getting started
Install [Material.Icons.WPF nuget package](https://www.nuget.org/packages/Material.Icons.WPF/):
```shell
dotnet add package Material.Icons.WPF
```
[![wpf-nuget](https://img.shields.io/nuget/v/Material.Icons.WPF?label=Material.Icons.WPF&style=flat-square)][wpf-nuget]
[![wpf-nuget](https://img.shields.io/nuget/dt/Material.Icons.WPF?color=blue&label=Downloads&style=flat-square)][wpf-nuget]
#### Using
Add `Material.Icons.WPF` namespace to the root element of your file (your IDE can suggest it or do it automatically):
```
xmlns:materialIcons="clr-namespace:Material.Icons.WPF;assembly=Material.Icons.WPF"
```
Use `MaterialIcon` control:
```xaml
<materialIcons:MaterialIcon Kind="Abacus" />
```
The `Foreground` property controls the color of the icon.  
Also, there is `MaterialIconExt` which allows you to use is as the markup extension:
```xaml
<Button Content="{materialIcons:MaterialIconExt Kind=Abacus}" />
```


## Meta
#### Getting started
Install [Material.Icons nuget package](https://www.nuget.org/packages/Material.Icons/):
```shell
dotnet add package Material.Icons
```
[![icons-nuget](https://img.shields.io/nuget/v/Material.Icons?label=Material.Icons&style=flat-square)][icons-nuget]
[![icons-nuget](https://img.shields.io/nuget/dt/Material.Icons?color=blue&label=Downloads&style=flat-square)][icons-nuget]
#### Using
Icon types stored in `Material.Icons.MaterialIconKind` enum.  
We can resolve an icon path by using `Material.Icons.MaterialIconDataProvider.GetData()`.  

#### Adding your own icons
Currently, there is no way to add your own icons, as icons are enum and cannot be modified.  
But you can override some existing icons to use your own data: 
```csharp
public class CustomIconProvider : MaterialIconDataProvider
{
    public override string ProvideData(MaterialIconKind kind)
    {
        return kind switch
        {
            MaterialIconKind.TrophyVariant => "some SVG code",
            _ => base.ProvideData(kind)
        };
    }
}

// When your application starts (e.g. in the Main method) replace MaterialIconDataProvider with your own
public static int Main(string[] args)
{
    MaterialIconDataProvider.Instance = new CustomIconProvider(); // Settings custom provider

    // Application startup code
    // return BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
}
```

## FAQ
#### How to change icon color?
- Change `Foreground` property.
#### How to change size?
- If you are using `MaterialIcon` control - use `Width` or/and `Height` properties.
- If you are using `MaterialIconExt` - use `Size` property.
#### How to update icons?
- You can manually set `Material.Icons` package version in your project file.
#### What about versioning policy?
- We use semver.  
  Any package with identical major and minor versions is compatible.  
  For example, `1.0.0` and `1.0.1` are compatible, but `1.0.0` and `1.1.0` might not be.
