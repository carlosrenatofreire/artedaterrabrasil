using LayeredArch.Mvc.ViewModels.Shareds;
using Microsoft.AspNetCore.Mvc;

namespace LayeredArch.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");

            return RedirectToAction("Login", "Account");
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            if (id == 500)
            {
                modelError.Message = "(500) - Um erro de servidor ocorreu! Por favor contacte o suporte!";
                modelError.Title = "Um erro de servidor ocorreu!";
                modelError.ErrorCode = id;
            }
            else if (id == 401)
            {
                modelError.Message = "(401) - Voce n�o esta Autenticado!  Por favor contacte o suporte!";
                modelError.Title = "N�o Autenticado!";
                modelError.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelError.Message = "(403) - Voce n�o tem autoriza��o de aceder est� p�gina!  Por favor contacte o suporte!";
                modelError.Title = "N�o Autorizado!";
                modelError.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelError.Message = "(404) - A pagina que procura n�o existe! Por favor contacte o suporte!";
                modelError.Title = "Ops! Page not found!";
                modelError.ErrorCode = id;
            }
            else if (id == 408)
            {
                modelError.Message = "(408) - O tempo de execu��o excedeu o limite!  Por favor contacte o suporte!";
                modelError.Title = "Timeout";
                modelError.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelError);
        }

    }
}
