using Microsoft.AspNetCore.Mvc;

namespace LayeredArch.Mvc.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
