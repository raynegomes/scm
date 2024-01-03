namespace AppManager.API.Configurations.HealthCheck;

public static class HealthDatabase
{
	public static void AddHealthDatabase(
		this IServiceCollection services, 
		IConfiguration configuration
	)
	{
		services.AddHealthChecks()
			.AddNpgSql(
				npgsqlConnectionString: configuration.GetConnectionString("CommandsConnectionString") ?? string.Empty,
				name: "CommandsInstance",
				tags: new string[] { "db", "data" }
			)
			.AddNpgSql(
				npgsqlConnectionString: configuration.GetConnectionString("QueriesConnectionStrings") ?? string.Empty,
				name: "QueriesInstance",
				tags: new string[] { "db", "data" }
			);
	}
}
