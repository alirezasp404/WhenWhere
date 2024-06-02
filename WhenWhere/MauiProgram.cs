using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using WhenWhere.Models;
using WhenWhere.Pages;
using WhenWhere.ServiceContracts;
using WhenWhere.Services;
namespace WhenWhere
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Services.AddSingleton<SignIn>();
            builder.Services.AddSingleton<IEventsService,EventsService>();
            builder.Services.AddSingleton<IHttpClientBuilder,HttpClientBuilder>();
            builder.Services.AddSingleton<Events>();
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

            return builder.Build();
        }
    }
}
