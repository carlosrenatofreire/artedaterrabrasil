using System.ComponentModel;

namespace ArteDaTerraBrasil.Mvc.ViewModels.Shareds
{
    public class AuditLogViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Tipo de Log")]
        public int LogType { get; set; }
        public string UrlBase { get; set; }
        public string Endpoint { get; set; }
        public string Verb { get; set; }
        public string Ip { get; set; }
        [DisplayName("Data de Registro")]
        public DateTime RegisterDate { get; set; }
        public string Username { get; set; }
        [DisplayName("Mensagem")]
        public string Message { get; set; }
    }

}
