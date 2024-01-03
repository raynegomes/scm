namespace AppManager.API.Configurations.Cache;
public static class CacheConfig
{
	public static void AddCacheConfiguration(
		this IServiceCollection services,
		IConfiguration configuration
	)
	{
		if (!configuration.GetValue<bool>("CacheApplication:IsEnable")) return;

		services.AddStackExchangeRedisCache(opt =>
		{
			opt.InstanceName = configuration.GetValue<string>("CacheApplication:Prefix");
			opt.Configuration = configuration.GetConnectionString("CacheConnectionString");
		});
	}
}
