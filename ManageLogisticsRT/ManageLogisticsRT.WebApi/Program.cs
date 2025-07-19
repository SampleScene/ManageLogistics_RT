using ManageLogisticsRT.Data.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataContext(builder.Configuration);

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
