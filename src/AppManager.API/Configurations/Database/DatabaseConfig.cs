using Dapper;
using Microsoft.EntityFrameworkCore;

using AppManager.Infra.Data.Context;
using AppManager.Infra.Data.Mapping;

namespace AppManager.API.Configurations.Database;

public static class DatabaseConfig
{
	public static void AddDatabaseConfiguration(
		this IServiceCollection services,
		IConfiguration configuration
	)
	{
		DefaultTypeMap.MatchNamesWithUnderscores = true;

		services.AddDbContext<EfDbContext>(opt =>
			opt.UseNpgsql(
				configuration.GetConnectionString("CommandsConnectionString"),
				opt => opt.MigrationsHistoryTable(
					"_EfMigrationHistory",
					SchemaNames.DefaultSchema.Value
				)
			)
		);
		services.AddScoped<DapperDbContext>();
		services.AddScoped<EfDbContext>();


	}
}
