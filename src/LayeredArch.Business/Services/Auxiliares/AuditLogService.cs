using LayeredArch.Business.Interfaces.Auxiliares;
using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Auxiliares;
using LayeredArch.Business.Services.Shareds;

namespace LayeredArch.Business.Services.Auxiliares
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
