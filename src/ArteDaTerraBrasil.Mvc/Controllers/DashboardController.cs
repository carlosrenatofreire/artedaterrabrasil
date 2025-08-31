using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArteDaTerraBrasil.Mvc.Controllers
{
    public class DashboardController : Controller
    {
        [AllowAnonymous]
        [Route("Dashboard")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
