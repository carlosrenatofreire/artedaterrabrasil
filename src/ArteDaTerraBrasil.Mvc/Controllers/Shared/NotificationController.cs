using ArteDaTerraBrasil.Mvc.ViewModels.Shareds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArteDaTerraBrasil.Mvc.Controllers.Shared
{
    public class NotificationController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("Notification-Partial")]
        public async Task<IActionResult> Partial()
        {
            // Simula um delay de uma operação assíncrona
            await Task.Delay(100);

            // Mock de dados
            var mockLogs = new List<AuditLogViewModel>
            {
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 1,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/users",
                    Verb = "GET",
                    Ip = "192.168.0.12",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-5),
                    Username = "carlos.rfreire",
                    Message = "Acesso ao módulo de utilizadores."
                },
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 3,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/auth/login",
                    Verb = "POST",
                    Ip = "192.168.0.99",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-15),
                    Username = "carlos.rfreire",
                    Message = "Erro de login: senha incorreta."
                },
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 2,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/roles",
                    Verb = "GET",
                    Ip = "192.168.0.20",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-25),
                    Username = "carlos.rfreire",
                    Message = "Consulta da lista de perfis."
                },
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 1,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/users",
                    Verb = "GET",
                    Ip = "192.168.0.12",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-5),
                    Username = "carlos.rfreire",
                    Message = "Acesso ao módulo de utilizadores."
                },
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 3,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/auth/login",
                    Verb = "POST",
                    Ip = "192.168.0.99",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-15),
                    Username = "carlos.rfreire",
                    Message = "Erro de login: senha incorreta."
                },
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 2,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/roles",
                    Verb = "GET",
                    Ip = "192.168.0.20",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-25),
                    Username = "carlos.rfreire",
                    Message = "Consulta da lista de perfis."
                },
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 1,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/users",
                    Verb = "GET",
                    Ip = "192.168.0.12",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-5),
                    Username = "carlos.rfreire",
                    Message = "Acesso ao módulo de utilizadores."
                },
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 3,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/auth/login",
                    Verb = "POST",
                    Ip = "192.168.0.99",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-15),
                    Username = "carlos.rfreire",
                    Message = "Erro de login: senha incorreta."
                },
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 2,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/roles",
                    Verb = "GET",
                    Ip = "192.168.0.20",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-25),
                    Username = "carlos.rfreire",
                    Message = "Consulta da lista de perfis."
                },
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 1,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/users",
                    Verb = "GET",
                    Ip = "192.168.0.12",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-5),
                    Username = "carlos.rfreire",
                    Message = "Acesso ao módulo de utilizadores."
                },
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 3,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/auth/login",
                    Verb = "POST",
                    Ip = "192.168.0.99",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-15),
                    Username = "carlos.rfreire",
                    Message = "Erro de login: senha incorreta."
                },
                new AuditLogViewModel
                {
                    Id = Guid.NewGuid(),
                    LogType = 2,
                    UrlBase = "https://intranet.grupojap.pt",
                    Endpoint = "/api/roles",
                    Verb = "GET",
                    Ip = "192.168.0.20",
                    RegisterDate = DateTime.UtcNow.AddMinutes(-25),
                    Username = "carlos.rfreire",
                    Message = "Consulta da lista de perfis."
                }
            };


            return PartialView("_NotificationList", mockLogs);
        }
    }
}
