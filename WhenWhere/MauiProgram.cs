using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using WhenWhere.Services;

namespace WhenWhere
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        PInvoke.User32.ShowWindow(hWnd, PInvoke.User32.WindowShowStyle.SW_SHOWMAXIMIZED);


                    });
                });
            });

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
