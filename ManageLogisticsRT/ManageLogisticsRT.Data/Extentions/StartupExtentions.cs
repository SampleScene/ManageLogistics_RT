using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageLogisticsRT.Data.Extentions;

public static class StartupExtentions
{
   public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
   {
        services.AddDbContext<DataContext>(options => options.UseNpgsql(configuration.GetConnectionString(nameof(DataContext))));
        return services;
   }
}