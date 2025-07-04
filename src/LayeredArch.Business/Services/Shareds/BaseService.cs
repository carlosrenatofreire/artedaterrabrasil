using FluentValidation;
using FluentValidation.Results;
using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Shareds;

namespace LayeredArch.Business.Services.Shareds
{
    public abstract class BaseService
    {
        private readonly INotifierService _notifier;
        private readonly IUnitOfWork _unitOfWork;

        protected BaseService(INotifierService notifier,
                              IUnitOfWork unitOfWork)
        {
            _notifier = notifier;
            _unitOfWork = unitOfWork;
        }
        protected bool RunValidation<VT, VE>(VT validationType, VE validationEntity)
                               where VT : AbstractValidator<VE>
                               where VE : Entity
        {
            var validateResult = validationType.Validate(validationEntity);

            if (validateResult.IsValid) return true;

            NotifyWithMessage(validateResult); // Launch Notifications

            return false;
        }
        protected void NotifyWithMessage(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                NotifyWithMessage(item.ErrorMessage);
            }
        }

        protected void NotifyWithMessage(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected async Task<bool> Commit()
        {
            if (await _unitOfWork.CommitAsync()) return true;

            NotifyWithMessage("Não foi possível salvar os dados no banco! ");

            return false;
        }

    }
}
