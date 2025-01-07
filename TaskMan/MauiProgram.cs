using Microsoft.Extensions.Logging;
using TaskMan.Interfaces;
using TaskMan.Services;
using TaskMan.ViewModels;
using TaskMan.Views;

namespace TaskMan
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<ISqliteService, SqliteService>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AddPageViewModel>();
            builder.Services.AddTransient<AddPage>();
            return builder.Build();
        }
    }
}
