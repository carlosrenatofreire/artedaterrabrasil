using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArteDaTerraBrasil.Mvc.Controllers.Shared
{
    public class AuditLogController : Controller
    {
        [AllowAnonymous]
        [Route("Audit-List")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
