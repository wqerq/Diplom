using CommunityToolkit.Maui;
using Diplom.Helpers;
using Diplom.Models;
using Diplom.Services;
using Diplom.ViewModels;
using Diplom.Views;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;

namespace Diplom
{
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
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit().
                UseMauiCommunityToolkitMediaElement();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts();
            builder.Services.AddTransient<StartViewModel>();
            builder.Services.AddTransient<StartPage>();
            builder.Services.AddTransient<SessionViewModel>();
            builder.Services.AddTransient<SessionPage>();
            builder.Services.AddSingleton<TaskBankService>();
            builder.Services.AddSingleton<TaskGeneratorService>();
            builder.Services.AddSingleton<IResultStorage, FileResultStorage>();
            builder.Services.AddTransient<SessionViewModel>();
            builder.Services.AddTransient<StatsViewModel>();
            builder.Services.AddTransient<StatsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
