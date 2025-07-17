using ManageLogistics_RT.Data;
using ManageLogistics_RT.Models;
using ManageLogistics_RT.ViewModels.TripViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ManageLogistics_RT.Controllers
{
    public class TripController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TripController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, RoleManager<IdentityRole> roleManager) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> AllTrip() 
        {
            if (User.Identity.IsAuthenticated && !User.IsInRole("user"))
            {
                var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
                var employee = await _context.employees.FirstOrDefaultAsync(i => i.EmployeeId == userId);
                var trips = await _context.Trips.FromSqlRaw($"select * from Trips WHERE RouteId in (SELECT id from Ways where TerminalId = {employee.TerminalId});").ToListAsync();
                return View(trips);

               // var trips = _context.Trips.Where(t => t.).ToListAsync();
            }
            return View("EmployeePreview", "Dashboard");
        }
        
        public async Task<IActionResult> Create(CreateTripViewModel createTripViewModel)
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var creator = await _context.employees.FirstOrDefaultAsync(i => i.EmployeeId == userId);
            var driverRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == "driver");
            var driver = await _context.employees.FromSqlRaw($"SELECT * FROM employees WHERE EmployeeId IN (SELECT UserId from AspNetUserRoles WHERE RoleId = \"{driverRole.Id}\")  AND TerminalId = {creator.TerminalId}").ToListAsync();
            var routes = await _context.Ways.FromSqlRaw($"SELECT * FROM Ways WHERE TerminalId = {creator.TerminalId}").ToListAsync();
            var buses = await _context.Buses.FromSqlRaw($"SELECT * FROM Buses WHERE TerminalId ={creator.TerminalId}").ToListAsync();

            List<SelectListItem> listDrivers = new List<SelectListItem>();
            foreach ( var item in driver )
            {
                listDrivers.Add(new SelectListItem { Value = $"{item.EmployeeId}", Text = $"{item.FirstName + " " + item.LastName[0] + "."}" });
            }

            List<SelectListItem> listRoutes = new List<SelectListItem>();
            foreach (var item in routes)
            {
                listRoutes.Add(new SelectListItem { Value = $"{item.Id}", Text = $"Мрашрут {item.Id}" });
            }
            
            List<SelectListItem> listBuses = new List<SelectListItem>();
            foreach (var item in buses)
            {
                listBuses.Add(new SelectListItem { Value = $"{item.Id}", Text = $"#{item.Id} Модель:{item.Model}" });
            }

            var newcreateTripViewModel = new CreateTripViewModel()
            {
                Drivers = listDrivers,
                Routes = listRoutes,
                Buses = listBuses,
                MessageError = createTripViewModel.MessageError,
                
            };
            return View(newcreateTripViewModel);
        }
        
        
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateTrip(CreateTripViewModel createTripViewModel)
        {
            if (!ModelState.IsValid)
            {
                createTripViewModel.MessageError = "Ошибка добавления. Необходимо заполнить все поля";
                return RedirectToAction("Create", "Trip", createTripViewModel);
            }
            
            DateTime TimeStart = DateTime.Now;
            TimeStart = TimeStart.AddDays(1);
            
            if (createTripViewModel.TimeStart <= TimeStart)
            {
                createTripViewModel.MessageError = "Дата начала должна быть указана за день до наступления";
                return RedirectToAction("Create", "Trip", createTripViewModel);
            }
            
            var driverTrips = await _context.Trips.Where(d => d.DriverId == createTripViewModel.SelectedDriver).ToListAsync();
            foreach (var item in driverTrips)
            {
                if (item.TimeEnd >= createTripViewModel.TimeStart)
                {
                    createTripViewModel.MessageError = "На это время водитель уже занят";
                    return RedirectToAction("Create", "Trip", createTripViewModel);
                }
            }
            
            var stopsOnRoute = await  _context.StopsOnRoutes.FromSqlRaw($"SELECT * FROM StopsOnRoutes WHERE RouteId = {createTripViewModel.SelectedRoute}").ToListAsync();
            int minutes = 0;
            foreach (var stop in stopsOnRoute)
            {
                minutes += stop.TransferTime;
            }
            minutes = minutes * 2 + 5;
            DateTime TimeEnd = createTripViewModel.TimeStart;
            TimeEnd =  TimeEnd.AddMinutes(minutes);
            
            var trip = new Trip()
            {
                DriverId = createTripViewModel.SelectedDriver,
                RouteId = createTripViewModel.SelectedRoute,
                BusId = createTripViewModel.SelectedBus,
                TimeStart = createTripViewModel.TimeStart,
                TimeEnd = TimeEnd,
            };
           
            _context.Trips.Add(trip);
            _context.SaveChanges();

            return RedirectToAction("AllTrip", "Trip");

            //return $"{createTripViewModel.SelectedDriver}   {createTripViewModel.SelectedRoute}    {createTripViewModel.SelectedBus}   {createTripViewModel.TimeStart}";
        }
    }
}
