using Microsoft.AspNetCore.Localization;

using System.Globalization;

namespace AppManager.API.Configurations.Language;

public static class LanguagesConfig
{
	public static void AddLanguages(this IServiceCollection services)
	{
		var supportedCultures = new List<CultureInfo>
		{
			new CultureInfo("pt-BR"),
			new CultureInfo("en"),
			new CultureInfo("es")
		};

		services.AddLocalization();

		services.Configure<RequestLocalizationOptions>(opt =>
		{
			opt.SupportedCultures = supportedCultures;
			opt.SupportedUICultures = supportedCultures;
			opt.DefaultRequestCulture = new RequestCulture(
				culture: supportedCultures[0],
				uiCulture: supportedCultures[0]
			);
		});
	}
}
