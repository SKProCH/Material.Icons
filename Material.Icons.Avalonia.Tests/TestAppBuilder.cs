using Avalonia;
using Avalonia.Headless;
using Material.Icons.Avalonia.Tests;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace Material.Icons.Avalonia.Tests;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseSkia()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}

public class App : Application
{
    public override void Initialize()
    {
        Styles.Add(new MaterialIconStyles(null));
    }
}
