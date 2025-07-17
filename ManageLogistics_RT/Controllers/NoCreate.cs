using Microsoft.AspNetCore.Mvc;

namespace ManageLogistics_RT.Controllers
{
    public class NoCreate : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
