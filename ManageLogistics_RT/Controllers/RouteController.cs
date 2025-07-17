using ManageLogistics_RT.Data;
using ManageLogistics_RT.Models;
using ManageLogistics_RT.Repository;
using ManageLogistics_RT.ViewModels.Route;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManageLogistics_RT.Controllers
{
    public class RouteController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public RouteController( ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<IActionResult> CreateRoute()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var user = await _context.employees.FirstOrDefaultAsync(i => i.EmployeeId == curUserId);
            var Terminal = await _context.terminals.FirstOrDefaultAsync(i => i.Id == user.TerminalId);
            var stops = await _context.stops.Where(c => c.City == Terminal.City).ToListAsync();
            var newCreateRouteViewmodel = new CreateRouteViewModel()
            {
                terminalId = Terminal.Id,
                stops = stops,
            };
            return View(newCreateRouteViewmodel);
        }
    }
}