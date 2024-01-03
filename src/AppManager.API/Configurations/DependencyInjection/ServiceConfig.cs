namespace AppManager.API.Configurations.DependencyInjection;

public static class ServiceConfig
{
	public static void ConfigureDependenciesService(this IServiceCollection services)
	{
		services.AddUserServicesInjection();
	}
}
