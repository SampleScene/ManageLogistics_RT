using Microsoft.AspNetCore.Mvc;
using ManageLogistics_RT.ViewModels.Employee;
using ManageLogistics_RT.Models;
using Microsoft.AspNetCore.Identity;
using ManageLogistics_RT.Data;
using ManageLogistics_RT.Data.Enum;
using Newtonsoft.Json;
using ManageLogistics_RT.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ManageLogistics_RT.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UserAuthData> _userManager;
        private readonly ApplicationDbContext _context;

        public EmployeeController(IHttpContextAccessor httpContextAccessor, UserManager<UserAuthData> userManager, ApplicationDbContext context) 
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> EmployeeList()
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var employee = await _context.employees.FirstOrDefaultAsync(i => i.EmployeeId == userId);
            var employees = await _context.employees.Where(t => t.TerminalId == employee.TerminalId).ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(string id)
        {
            var employee = await _context.employees.FirstOrDefaultAsync(i => i.EmployeeId == id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit stop");
            }

            var currentEmployee = await _context.employees.AsNoTracking().FirstOrDefaultAsync(i => i.EmployeeId == employee.EmployeeId);

            if(currentEmployee != null)
            {
                var createdEmployee = new Employee
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    TerminalId = employee.TerminalId,
                    SerisesAndNumber = employee.SerisesAndNumber,
                    IssuedBy = employee.IssuedBy
                };
                _context.employees.Update(createdEmployee);
                _context.SaveChanges();
                return RedirectToAction("EmployeeList", "Employee");
            }
            return View(employee);
        }

        
        public IActionResult CreateEmployee()
        {
            var response = new CreateEmployeeViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeViewModel createEmployeeVM)
        {

            if (!ModelState.IsValid) return View(createEmployeeVM);

            var employeeCheck = await _userManager.FindByEmailAsync(createEmployeeVM.EmailAddress);
            if (employeeCheck != null)
            {
                TempData["Error"] = "Этот адрес электронной почты уже используется";
                return View(createEmployeeVM);
            }

            var newUser = new UserAuthData()
            {
                UserName = createEmployeeVM.EmailAddress,
                Email = createEmployeeVM.EmailAddress,
                EmailConfirmed = true
            };
            var newUserRsponse = await _userManager.CreateAsync(newUser, createEmployeeVM.Password);



            if (newUserRsponse.Succeeded)
            {
                JsonResult jsonResult = Json(new { data = new { id = newUser.Id } });
                string jsonString = JsonConvert.SerializeObject(jsonResult.Value);
                var UserData = JsonConvert.DeserializeObject<dynamic>(jsonString);

                var employee = new Employee()
                {
                    EmployeeId = UserData.data.id,
                    TerminalId = createEmployeeVM.TerminalId,
                    FirstName = createEmployeeVM.FirstName,
                    LastName = createEmployeeVM.LastName,
                    SerisesAndNumber = createEmployeeVM.SerialAndNumber,
                    IssuedBy = createEmployeeVM.IssuedBy,
                };

                _context.employees.Add(employee);
                _context.SaveChanges();
            }
            
            if (newUserRsponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, createEmployeeVM.Roles.ToString());

            return RedirectToAction("Index", "Home");
        }
    }
}