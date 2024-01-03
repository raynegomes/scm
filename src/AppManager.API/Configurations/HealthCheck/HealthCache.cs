namespace AppManager.API.Configurations.HealthCheck;

public static class HealthCache
{
	public static void AddHealthCache(
		this IServiceCollection services, 
		IConfiguration configuration
	)
	{
		if (!configuration.GetValue<bool>("CacheApplication:IsEnable")) return;

		services.AddHealthChecks().AddRedis(
				redisConnectionString: configuration.GetConnectionString("CacheConnectionString") ?? string.Empty,
				name: "RedisCacheInstance",
				tags: new string[] { "db", "cache" }
			);
	}
}
