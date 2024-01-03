namespace AppManager.API.Configurations;

public static class MapperConfig
{
	public static void AddMapper(this IServiceCollection services)
	{
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
	}
}
