using LayeredArch.Business.Interfaces.Auxiliares;
using LayeredArch.Business.Models.Auxiliares;
using LayeredArch.Data.Contexts;
using LayeredArch.Data.Repository.Shareds;

namespace LayeredArch.Data.Repository.Auxiliares
{
    public class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(MyDbContext context) : base(context)
        {
        }

    }
}
