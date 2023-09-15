using Microsoft.AspNetCore.Components.WebView.Maui;
using MAUI_MasterDetails.Data;
using MAUI_MasterDetails.Service;

namespace MAUI_MasterDetails;

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
			});

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddSingleton<BookingService>();

        return builder.Build();
	}
}
