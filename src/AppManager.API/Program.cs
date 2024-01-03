using Carter;
using Microsoft.Extensions.Options;
using Serilog;

using AppManager.API.Configurations;
using AppManager.API.Configurations.DependencyInjection;
using AppManager.API.Configurations.HealthCheck;
using AppManager.API.Configurations.Cache;
using AppManager.API.Configurations.Database;
using AppManager.API.Configurations.Language;
using AppManager.API.Configurations.Logs;

try
{
	var builder = WebApplication.CreateBuilder(args);

	#region ConfigureServices

	// HealthChecker
	builder.Services.AddHealthConfiguration(builder.Configuration);

	// Languages
	builder.Services.AddLanguages();

	// Dependencies Service
	builder.Services.ConfigureDependenciesService();

	// Logger
	SerilogConfig.AddSerilogConfig();
	builder.Logging.ClearProviders();
	builder.Logging.AddSerilog(Log.Logger);

	// Mapper
	builder.Services.AddMapper();

	builder.Services.AddEndpointsApiExplorer();

	// Databases
	builder.Services.AddDatabaseConfiguration(builder.Configuration);
	// Cache
	builder.Services.AddCacheConfiguration(builder.Configuration);
	// Swagger
	builder.Services.AddSwaggerConfiguration(builder.Configuration);
	// Carter Routes
	builder.Services.AddCarter();

	#endregion

	var app = builder.Build();

	#region Configure

	if (app.Environment.IsDevelopment())
	{
		app.UseDeveloperExceptionPage();
		app.UseSwaggerSetup();
	}

	app.UseHttpsRedirection();
	app.MapCarter();

	var localizeOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
	app.UseRequestLocalization(localizeOptions.Value);

	app.UseHealthChecksSetup();

	#endregion

	string baseUrl = "https://localhost:5001";

	Log.Information("HealthCheck: {baseUrl}/health", baseUrl);
	Log.Information("Application: {baseUrl}/api/swagger", baseUrl);

	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Host terminated unexpectedly");
}
finally 
{
	Log.Information("Server shutting down...");
	Log.CloseAndFlush();
}