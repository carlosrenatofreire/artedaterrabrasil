using Microsoft.AspNetCore.Mvc;

namespace ArteDaTerraBrasil.Mvc.Controllers.Shared
{
    public class ErrorController : Controller
    {
        [Route("Error/403")]
        public IActionResult AccessDenied()
        {
            return View("403");
        }

        [Route("Error/404")]
        public IActionResult PageNotFound()
        {
            return View("404");
        }

        [Route("Error/500")]
        public IActionResult InternalServerError()
        {
            return View("500");
        }

        [Route("Error/{code:int}")]
        public IActionResult Error(int code)
        {
            return code switch
            {
                403 => RedirectToAction("AccessDenied"),
                404 => RedirectToAction("PageNotFound"),
                500 => RedirectToAction("InternalServerError"),
                _ => View("GenericError") // se quiser algo genérico
            };
        }
    }
}
