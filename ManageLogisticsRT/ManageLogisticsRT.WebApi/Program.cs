using ManageLogisticsRT.Data;
using ManageLogisticsRT.WebApi.Extentions;

namespace ManageLogisticsRT.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var app = CreateHostBuilder(args).Build();
        CheckDbConection(app);
        app.Run();  
    }
    
    public static WebApplicationBuilder CreateHostBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.SetupApplicationServices(builder.Configuration);
        return builder;
    }

    public static void CheckDbConection(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var log = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        try
        {
            dbContext.Database.CanConnect();
            log.LogInformation($"Db conect success");
        }
        catch (Exception ex)
        {
            log.LogError($"Db conect Failer: {ex.Message}");
        }
    }
} 