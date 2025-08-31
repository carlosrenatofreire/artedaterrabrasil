using ArteDaTerraBrasil.Business.Models.Auxiliares;

namespace ArteDaTerraBrasil.Business.Interfaces.Auxiliares
{
    public interface IAuditLogService : IDisposable
    {
        Task Add(AuditLog log);
    }
}
