using ManageLogistics_RT.Data;
using ManageLogistics_RT.Models;
using ManageLogistics_RT.ViewModels.PriceVM;
using ManageLogistics_RT.ViewModels.UserDashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.Json;

namespace ManageLogistics_RT.Controllers
{
    public class TripRecieptsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TripRecieptsController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Buy(string id)
        {
            
            if (User.Identity.IsAuthenticated && User.IsInRole("driver"))
            {
                var userCheck = _context.appUsers.AsNoTracking().FirstOrDefault(i => i.UserId == id);
                if (userCheck == null) return RedirectToAction("Index", "TripReciepts");
                
                var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
                var employee = _context.employees.FirstOrDefault(i => i.EmployeeId == userId);
                var currentRoute = _context.Trips.FromSqlRaw($"SELECT * FROM Trips WHERE `DriverId` = '{employee.EmployeeId}' and ( CURRENT_TIMESTAMP > TimeStart and CURRENT_TIMESTAMP < TimeEnd)").FirstOrDefault();
                
                //var userPrices = await _context.passReciepts.FromSqlRaw($"SELECT * FROM passReciepts WHERE AppUserId = \"{userCheck.UserId}\"").Include(p => p.Price).ToListAsync();
                //var allUserPrices = await _context.passReciepts.Where(a => a.AppUserId == userCheck.UserId).Include(p => p.Price).ToListAsync();
                //if (allUserPrices == null) return RedirectToAction("Index", "TripReciepts");
               
                //string message = "";
                //string passesId = "0";
                //List<PassReciept> passForeCurrentTerminal = new List<PassReciept>();
                //foreach (var item in userPrices)
                //{
                //    if (item.Price.TerminalId == employee.TerminalId)
                //    {
                //        passForeCurrentTerminal.Add(item);
                //        passesId += ("," + item.Id.ToString());
                //    }
                //}
                
                var normTimePasses = await _context.passReciepts.FromSqlRaw($"SELECT passReciepts.Id, passReciepts.AppUserId, passReciepts.PriceId,passReciepts.TimeStump" +
                    $" from passReciepts, prices WHERE prices.Id = passReciepts.PriceId and TIMESTAMPDIFF(HOUR, passReciepts.TimeStump, CURRENT_TIMESTAMP) < prices.Time" +
                    $" AND prices.Number IS NULL and passReciepts.AppUserId = '{userCheck.UserId}' order by passReciepts.TimeStump ASC").ToListAsync();
                
                if (normTimePasses.Count > 0)
                {
                    foreach (var item in normTimePasses)
                    {
                        var TripReciepts = new TripReceipt()
                        {
                            DateOfoperation = DateTime.Now,
                            Operation = "travelСard",
                            TripId = currentRoute.Id,
                            PassId = item.Id
                        };
                        _context.TripReceipts.Add(TripReciepts);
                        _context.SaveChanges();
                        //Оплата временным проездным
                        return RedirectToAction("Index", "TripReciepts");
                    }
                }
                
                var normNumberPasses = await _context.passReciepts.FromSqlRaw($"SELECT EndCount.Id, EndCount.AppUserId, EndCount.PriceId, EndCount.TimeStump" +
                    $" from prices, (SELECT passReciepts.Id, passReciepts.AppUserId, passReciepts.PriceId, passReciepts.TimeStump, PassCount.Poezdki" +
                    $" from passReciepts, (SELECT PassId, count(Id) as Poezdki FROM TripReceipts GROUP by PassId HAVING PassId in(2,9) ) as PassCount" +
                    $" WHERE passReciepts.Id = PassCount.PassId) as EndCount WHERE EndCount.PriceId = prices.Id and EndCount.Poezdki < prices.Number " +
                    $"and prices.Time is null").ToListAsync();
                 
                if (normNumberPasses.Count > 0)
                {
                    foreach (var item in normNumberPasses)
                    {
                        var TripReciepts = new TripReceipt()
                        {
                            DateOfoperation = DateTime.Now,
                            Operation = "travelСard",
                            TripId = currentRoute.Id,
                            PassId = item.Id
                        };
                        _context.TripReceipts.Add(TripReciepts);
                        _context.SaveChanges();
                        //Оплата колличественнм проездным
                        return RedirectToAction("Index", "TripReciepts");
                    }
                }

                if (userCheck != null)
                {
                    int fare = 30;
                    if (userCheck.Balance >= fare)
                    {
                        var userupdae = new AppUser()
                        {
                            UserId = userCheck.UserId,
                            FirstName = userCheck.FirstName,
                            LastName = userCheck.LastName,
                            Image = userCheck.Image,
                            Birthday = userCheck.Birthday,
                            Balance = userCheck.Balance - fare,
                        };

                        _context.appUsers.Update(userupdae);
                        _context.SaveChanges();

                        var TripReciepts = new TripReceipt()
                        {
                            DateOfoperation = DateTime.Now,
                            Operation = "ticket",
                            TripId = currentRoute.Id
                        };
                        _context.TripReceipts.Add(TripReciepts);
                        _context.SaveChanges();

                        //Разовый билет
                        return RedirectToAction("Index", "TripReciepts");
                    }
                    else
                    {
                        //Недостаточно средств
                        return RedirectToAction("Index", "TripReciepts");
                    }
                }
                //Пользователь не найден
                return RedirectToAction("Index", "TripReciepts");
            }
            else //Пользователь не авторизован
                return RedirectToAction("Index", "TripReciepts");
        }
        
        public async Task<IActionResult> UserTripReciepts()
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userPassReciepts = await _context.passReciepts.AsNoTracking().Include(p => p.Price).Where(i => i.AppUserId == userId).ToListAsync();
            var userTrips = await _context.TripReceipts.FromSqlRaw($"SELECT * from TripReceipts where PassId in (SELECT id FROM passReciepts where AppUserId = '{userId}')").AsNoTracking().ToListAsync();

            var tripViewModel = new List<UserTripRecieptsViewmodel>(); 

            foreach (var pass in userPassReciepts)
            {
                foreach(var trip in userTrips)
                {
                    if (trip.PassId == pass.Id)
                    {
                        var userTrip = new UserTripRecieptsViewmodel()
                        {
                            Id = trip.Id,
                            Pass = pass.Price.Title,
                            TripId = trip.TripId,
                            DateOfoperation = trip.DateOfoperation,
                            Operation = trip.Operation
                        };
                        tripViewModel.Add(userTrip);
                    }
                }
            }
            
            return View(tripViewModel);
        }
    }
}
