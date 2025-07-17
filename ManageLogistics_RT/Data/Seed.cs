using ManageLogistics_RT.Models;
using Microsoft.AspNetCore.Identity;

namespace ManageLogistics_RT.Data
{
    //Also: 
    //Role:user ; Login:User1@gmail.com; Password: Coding@1234?;
    
    public class Seed
    {
        /*
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();
        
                //Terminals
                if (!context.terminals.Any())
                {
                    context.terminals.AddRange(new List<Terminal>()
                    {
                        new Terminal()
                        {
                            Title = "Тестовый",рtreet\":\"Cherepahina\",\"Home\":\"186\"}",
                            Capacity = 10
                        }
                    });
                    context.SaveChanges();
                }
                
                //Employees
                if (!context.employees.Any())
                {
                    context.employees.AddRange(new List<Employee>()
                    {
                        new Employee()
                        {
                          EmployeeId = 1,
                          TerminalId = 1,
                          FirstName = "Test1",
                          LastName = "Test1",
                          Pasport = "{\"Series\":\"03 19\",\"Number\":\"017733\"}",
                          Image = "https://i.pinimg.com/736x/8f/3e/2c/8f3e2c90af8c2c7c4d37ba3e386c5110.jpg"
                        }
                    });
                    context.SaveChanges();
                }

                //Prices
                if (!context.prices.Any())
                {
                    context.prices.AddRange(new List<Price>()
                    {
                        new Price()
                        {
                             Title = "Почасовой",
                             TerminalId = 1,
                             Type = Enum.TypePrice.TimePrice,
                             Fare = 150,
                             Time = 3
                        }
                    });
                    context.SaveChanges();
                }
               
                //Stop
                if (!context.stops.Any())
                {
                    context.stops.AddRange(new List<Stop>()
                    {
                        new Stop()
                        {
                            Address = "{\"City\":\"Rostov-on-Don\",\"Street\":\"Cherepahina\",\"Home\":\"186\"}",
                            Image = "https://img.tsargrad.tv/cache/a/e/13_jpg_43.jpeg/w720h405fill.jpeg"
                        }
                    });
                    context.SaveChanges();
                }
            } 

        }
            public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
            {
                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    //Roles
                    
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    if (!await roleManager.RoleExistsAsync(UserRoles.GlobalAdmin))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.GlobalAdmin));
                    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                    if (!await roleManager.RoleExistsAsync(UserRoles.Driver))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.Driver));
                    if (!await roleManager.RoleExistsAsync(UserRoles.Logistician))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.Logistician));
                    if (!await roleManager.RoleExistsAsync(UserRoles.User))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                    
                    //Users
                    
                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<UserAuthData>>();
                
                    string adminUserEmail = "rubanartem823@gmail.com";

                    var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                    if (adminUser == null)
                    {
                        var newGlobaAdminUser = new UserAuthData()
                        {
                            UserName = "KASHKA",
                            Email = adminUserEmail,
                            EmailConfirmed = true
                        };
                        await userManager.CreateAsync(newGlobaAdminUser, "Coding@1234?");
                        await userManager.AddToRoleAsync(newGlobaAdminUser, UserRoles.GlobalAdmin);
                    }
               
                    string appUserEmail = "boykodaniil58@gmail.com";

                    var appUser = await userManager.FindByEmailAsync(appUserEmail);
                    if (appUser == null)
                    {
                        var newAppUser = new UserAuthData()
                        {
                            UserName = "kogun",
                            Email = appUserEmail,
                            EmailConfirmed = true
                        };
                        await userManager.CreateAsync(newAppUser, "");
                        await userManager.AddToRoleAsync(newAppUser, UserRoles.Admin);
                    } 
                } 
            }*/
    }
}