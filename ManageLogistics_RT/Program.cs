using ManageLogistics_RT.Data;
using ManageLogistics_RT.Helpers;
using ManageLogistics_RT.Interface;
using ManageLogistics_RT.Models;
using ManageLogistics_RT.Repository;
using ManageLogistics_RT.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IStopRepository, StopRepository>();
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IUserDashboardRepository, UserDashboardRepository>();
builder.Services.AddScoped<IQRCodeGenerator, QRCodeGeneratorHelper>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 3)));
});
builder.Services.AddIdentity<UserAuthData, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    //await Seed.SeedUsersAndRolesAsync(app);
    //Seed.SeedData(app);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();