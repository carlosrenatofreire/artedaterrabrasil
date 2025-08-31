using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArteDaTerraBrasil.Mvc.Controllers.Padroes
{
    public class StackController : Controller
    {
        [AllowAnonymous]
        [Route("Stack-List")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
