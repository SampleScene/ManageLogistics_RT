using ManageLogisticsRT.Data.Extentions;

namespace ManageLogisticsRT.WebApi.Extentions;

public static class StartupExtentions
{
    public static IServiceCollection SetupApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataContext(configuration);
        return services;
    }
}
