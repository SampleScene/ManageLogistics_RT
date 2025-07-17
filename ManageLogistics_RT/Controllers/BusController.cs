using ManageLogistics_RT.Data;
using ManageLogistics_RT.Interface;
using ManageLogistics_RT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageLogistics_RT.Controllers
{
    public class BusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBusRepository _busRepository;
        
        public BusController(ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IBusRepository busRepository) 
        {
            _httpContextAccessor = httpContextAccessor;
            _busRepository = busRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Bus> bus = await _busRepository.GetAll();
            return View(bus);
        }

        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated && !User.IsInRole("user"))
            {
                var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
                var Currentemp = _context.employees.FirstOrDefault(i => i.EmployeeId == userId);
                var bus = new Bus {
                    TerminalId = Currentemp.TerminalId,
                };
                return View(bus);
            }
            return RedirectToAction("Index", "Home");

        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Bus bus)
        {
            if (User.Identity.IsAuthenticated && !User.IsInRole("user"))
            {
                if (!ModelState.IsValid)
                {
                    return View(bus);
                }

                _context.Buses.Add(bus);
                _context.SaveChanges();
                return RedirectToAction("Index", "Bus");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
