using ArteDaTerraBrasil.Business.Models.Shareds;

namespace ArteDaTerraBrasil.Business.Models.Auxiliares
{
    public class AuditLog : Entity
    {
        public int LogType { get; set; }
        public string UrlBase { get; set; }
        public string Endpoint { get; set; }
        public string Verb { get; set; }
        public string Ip { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }

    }
}
