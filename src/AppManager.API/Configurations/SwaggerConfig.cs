using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace AppManager.API.Configurations;

public static class SwaggerConfig
{
	private const string SWAGGER_VERSION = "v1";
	private static string s_swaggerTitle = string.Empty;

	public static void AddSwaggerConfiguration(
		this IServiceCollection services, 
		IConfiguration configuration
	)
	{
		if (services is null) throw new ArgumentNullException(nameof(services));

		s_swaggerTitle = configuration.GetValue<string>("AppSettings:ApplicationName") ?? string.Empty;

		services.AddSwaggerGen(setup =>
		{
			var jwtSecurityScheme = new OpenApiSecurityScheme
			{
				Scheme = "bearer",
				BearerFormat = "JWT",
				Name = "JWT Authentication",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

				Reference = new OpenApiReference
				{
					Id = JwtBearerDefaults.AuthenticationScheme,
					Type = ReferenceType.SecurityScheme
				}
			};

			setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
			setup.AddSecurityRequirement(new OpenApiSecurityRequirement {
										{ jwtSecurityScheme, Array.Empty<string>() }
								});

			setup.SwaggerDoc(
					SWAGGER_VERSION,
					new OpenApiInfo
					{
						Title = s_swaggerTitle,
						Version = SWAGGER_VERSION,
						Description = string.Empty
					}
			);
		});
	}

	public static void UseSwaggerSetup(this IApplicationBuilder app)
	{
		if (app == null) throw new ArgumentNullException(nameof(app));

		app.UseSwagger(s => s.RouteTemplate = "api/swagger/{documentname}/swagger.json");

		app.UseSwaggerUI(s =>
		{
			s.SwaggerEndpoint("/api/swagger/v1/swagger.json", s_swaggerTitle);
			s.RoutePrefix = "api/swagger";
		});
	}
}
