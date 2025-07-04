using LayeredArch.Business.Interfaces.Parameters;
using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Parameters;
using LayeredArch.Business.Services.Shareds;
using LayeredArch.Business.Validations.Parameters;
using LayeredArch.Helper.StaticVariables;

namespace LayeredArch.Business.Services.Parameters
{
    public class ClaimService : BaseService, IClaimService
    {
        private readonly IClaimRepository _claimRepository;

        public ClaimService(IClaimRepository claimRepository,
                            INotifierService notifier,
                            IUnitOfWork unitOfWork) : base(notifier, unitOfWork)
        {
            _claimRepository = claimRepository;
        }

        public async Task AddAsync(Claim claim)
        {

            if (!RunValidation(new ClaimValidation(), claim)) return;

            await _claimRepository.AddAsync(claim);
            await Commit();
        }

        public async Task UpdateAsync(Claim claim)
        {
            if (!RunValidation(new ClaimValidation(), claim)) return;

            await _claimRepository.UpdateAsync(claim);
            await Commit();
        }

        public async Task RemoveAsync(Guid id)
        {
            var claim = await _claimRepository.GetByIdAsync(id);

            if (claim == null)
            {
                NotifyWithMessage(MessageHelper.GenericErrors.CouldntFindEntity("Claim", "id", id.ToString()));
                return;
            }

            // 3: Delete(Soft Delete)
            claim.Activated = false;
            await _claimRepository.UpdateAsync(claim);
            await Commit();
        }

        public async Task ActivatedAsync(Guid id)
        {
            var claim = await _claimRepository.GetByIdAsync(id);

            if (claim == null)
            {
                NotifyWithMessage(MessageHelper.GenericErrors.CouldntFindEntity("Claim", "id", id.ToString()));
                return;
            }

            if (claim.Activated)
            {
                NotifyWithMessage(MessageHelper.GenericErrors.EntityAlreadyActive("Claim"));
                return;
            }

            claim.Activated = true;
            await _claimRepository.UpdateAsync(claim);
            await Commit();
        }

        public void Dispose()
        {
            _claimRepository?.Dispose();
        }

    }
}
