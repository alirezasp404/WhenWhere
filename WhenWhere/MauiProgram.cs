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
            builder.Services.AddSingleton<IEventsService,EventsService>();
            builder.Services.AddSingleton<IAuthenticationService,AuthenticationService>();
            builder.Services.AddSingleton<IHttpClientBuilder,HttpClientBuilder>();
            builder.Services.AddSingleton<IUserInfoService,UserInfoService>();
            builder.Services.AddSingleton<Events>();
            builder.Services.AddSingleton<RegisteredEvents>();
            builder.Services.AddSingleton<Profile>();
            builder.Services.AddSingleton<CreateEvent>();
            builder.Services.AddSingleton<CreatedEvents>();
            builder.Services.AddSingleton<EventDetails>();
            builder.Services.AddSingleton<SignIn>();
            builder.Services.AddSingleton<SignUp>();
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
