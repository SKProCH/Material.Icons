using Microsoft.Extensions.Logging;

using ReactiveUI;

using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using Splat.Microsoft.Extensions.Logging;

namespace Material.Icons.Maui.Demo;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Initialization of Splat and ReactiveUI
        builder.Logging.AddSplat();
        builder.Services.UseMicrosoftDependencyResolver();
        Locator.CurrentMutable.InitializeSplat();
        Locator.CurrentMutable.InitializeReactiveUI();

        // ViewModels
        builder.Services.AddSingleton<MainViewModel>();

        // Views
        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Post-initialization of Splat
        app.Services.UseMicrosoftDependencyResolver();

        return app;
    }
}
