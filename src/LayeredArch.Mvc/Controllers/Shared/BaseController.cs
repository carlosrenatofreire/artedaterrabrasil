using LayeredArch.Business.Enums;
using LayeredArch.Business.Interfaces.Auxiliares;
using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Auxiliares;
using LayeredArch.Business.Models.Shareds;
using Microsoft.AspNetCore.Mvc;

namespace LayeredArch.Mvc.Controllers.Shared
{
    public abstract class BaseController : Controller
    {
        private readonly INotifierService _notifier;
        private readonly IAuditLogService _auditLogService;

        protected Guid UserId { get; set; }
        protected string UserName { get; set; }
        protected string Ip { get; set; }

        protected BaseController(INotifierService notifier,
                                 IAccessorService user,
                                 IAuditLogService auditLogService
                                 )
        {
            _notifier = notifier;

            if (user.IsAuthenticated())
            {
                UserId = user.GetUserId();
                UserName = user.GetUsername();
                Ip = user.GetRemoteIpAddress();
            }

            _auditLogService = auditLogService;
        }


        protected void NotifyErrorMessage(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected List<string> ModelStateResponse()
        {
            if (!ModelState.IsValid)
            {
                var warnings = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var warning in warnings) AuditLog(LogType.Warning, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, warning);
                return warnings;
            }


            return null; // Retorna null se não houver erro na ModelState
        }

        protected bool PersistenceValidation()
        {
            return !_notifier.HasNotification();
        }

        protected List<string> PersistenceResponse()
        {
            if (PersistenceValidation()) return null;

            var warnings = _notifier.GetNotification().Select(n => n.Message).ToList();

            foreach (var warning in warnings) AuditLog(LogType.Warning, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, warning);

            return warnings;
        }


        protected void AuditLog(LogType logType, string urlBase, string endpoint, string verb, string ip, string message)
        {
                try
                {
                    AuditLog auditLog = new AuditLog
                    {
                        LogType = (int)logType,
                        UrlBase = urlBase,
                        Endpoint = endpoint,
                        Verb = verb,
                        Ip = ip,
                        RegisterDate = DateTime.Now,
                        Username = UserName,
                        Message = message
                    };

                    _auditLogService.Add(auditLog);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }


        protected string FormattTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return tag;
            }

            // Trim leading and trailing whitespaces
            tag = tag.Trim();

            // Replace spaces with underscores and convert to lowercase
            return tag.Replace(" ", "_").ToLower();
        }

    }
}
