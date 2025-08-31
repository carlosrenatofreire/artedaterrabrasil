using ArteDaTerraBrasil.Business.Interfaces.Auxiliares;
using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Business.Models.Auxiliares;
using ArteDaTerraBrasil.Business.Services.Shareds;

namespace ArteDaTerraBrasil.Business.Services.Auxiliares
{ 
    public class AuditLogService : BaseService, IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly INotifierService _notifier;

        public AuditLogService(IAuditLogRepository auditLogRepository,
                               INotifierService notifier,
                               IUnitOfWork unitOfWork) : base(notifier, unitOfWork)
        {
            _notifier = notifier;
            _auditLogRepository = auditLogRepository;
        }

        public async Task Add(AuditLog log)
        {
            await _auditLogRepository.AddAsync(log);
            await Commit();
        }

        public void Dispose()
        {
            _auditLogRepository?.Dispose();
        }
    }
}
