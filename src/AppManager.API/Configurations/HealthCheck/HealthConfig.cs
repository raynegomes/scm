using System.Net.Mime;
using System.Text;
using System.Text.Json;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AppManager.API.Configurations.HealthCheck;

public static class HealthConfig
{
	public static void AddHealthConfiguration(
		this IServiceCollection services,
		IConfiguration configuration
	)
	{
		services.AddHealthDatabase(configuration);
		services.AddHealthCache(configuration);
	}

	public static void UseHealthChecksSetup(this IApplicationBuilder app)
	{
		app.UseHealthChecks("/health",
			new HealthCheckOptions()
			{
				Predicate = _ => true,
				ResultStatusCodes =
				{
					[HealthStatus.Healthy] = StatusCodes.Status200OK,
					[HealthStatus.Degraded] = StatusCodes.Status200OK,
					[HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
				},

				ResponseWriter = async (context, healthReport) =>
				{
					context.Response.ContentType = MediaTypeNames.Application.Json;
					var options = new JsonWriterOptions { Indented = true };

					using var memoryStream = new MemoryStream();

					using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
					{
						jsonWriter.WriteStartObject();
						jsonWriter.WriteString("Status", healthReport.Status.ToString());
						jsonWriter.WriteString("CurrentTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
						jsonWriter.WriteStartObject("Results");

						foreach (var healthReportEntry in healthReport.Entries)
						{
							jsonWriter.WriteStartObject(healthReportEntry.Key);
							jsonWriter.WriteString("Status",
									healthReportEntry.Value.Status.ToString());

							var msg = healthReportEntry.Value.Description
								?? healthReportEntry.Value.Exception?.Message;

							jsonWriter.WriteString("Message", msg);
							jsonWriter.WriteStartArray("Tags");

							foreach (var item in healthReportEntry.Value.Tags)
							{
								JsonSerializer.Serialize(jsonWriter, item);
							}

							jsonWriter.WriteEndArray();
							jsonWriter.WriteStartObject("Data");

							foreach (var item in healthReportEntry.Value.Data)
							{
								jsonWriter.WritePropertyName(item.Key);

								JsonSerializer.Serialize(jsonWriter, item.Value,
										item.Value?.GetType() ?? typeof(object));
							}

							jsonWriter.WriteEndObject();
							jsonWriter.WriteEndObject();
						}

						jsonWriter.WriteEndObject();
						jsonWriter.WriteEndObject();
					}

					await context.Response.WriteAsync(Encoding.UTF8.GetString(memoryStream.ToArray()));
				}
			}
		);

	}
}
