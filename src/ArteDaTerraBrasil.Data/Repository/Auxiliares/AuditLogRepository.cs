using ArteDaTerraBrasil.Business.Interfaces.Auxiliares;
using ArteDaTerraBrasil.Business.Models.Auxiliares;
using ArteDaTerraBrasil.Data.Contexts;
using ArteDaTerraBrasil.Data.Repository.Shareds;

namespace ArteDaTerraBrasil.Data.Repository.Auxiliares
{
    public class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(MyDbContext context) : base(context)
        {
        }

    }
}
