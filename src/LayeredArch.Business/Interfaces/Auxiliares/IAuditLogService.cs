using LayeredArch.Business.Models.Auxiliares;

namespace LayeredArch.Business.Interfaces.Auxiliares
{
    public interface IAuditLogService : IDisposable
    {
        Task Add(AuditLog log);
    }
}
