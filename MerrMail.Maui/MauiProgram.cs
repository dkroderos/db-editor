using MerrMail.Maui.Models;
using MerrMail.Maui.Services;
using MerrMail.Maui.ViewModels;
using MerrMail.Maui.Views;
using Microsoft.Extensions.Logging;

namespace MerrMail.Maui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IEmailContextService, EmailContextService>();
        builder.Services.AddSingleton<ISettings, Settings>();
        builder.Services.AddSingleton<EmailContextsViewModel>();
        builder.Services.AddSingleton<EmailContextsPage>();
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<HomePage>();

        return builder.Build();
    }
}
