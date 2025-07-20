using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageLogisticsRT.Data.Extentions;

public static class StartupExtentions
{
   public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
   {
        var connectionString = configuration.GetConnectionString(nameof(DataContext));
        services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
        return services;
   }
}