using Microsoft.Extensions.Logging;
using ExploradorJuegos.Services;
using ExploradorJuegos.ViewModels;
using ExploradorJuegos.Views;
namespace ExploradorJuegos
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
            builder.Services.AddHttpClient<IApiService, ApiService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
            });
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<DetalleJuegoViewModel>();
            builder.Services.AddTransient<DetalleJuegoPage>();
            return builder.Build();
        }
    }
}
