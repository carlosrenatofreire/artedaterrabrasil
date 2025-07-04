using FluentValidation;
using LayeredArch.Business.Models.Parameters;
using LayeredArch.Helper.StaticVariables;

namespace LayeredArch.Business.Validations.Parameters
{
    public class ClaimValidation : AbstractValidator<Claim>
    {
        public ClaimValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(MessageHelper.RequiredField);

            RuleFor(x => x.ModuleId)
                .NotEmpty()
                .WithMessage(MessageHelper.RequiredField);

            RuleFor(x => x.Description)
               .MaximumLength(500)
               .WithMessage(MessageHelper.RequireMax);

            RuleFor(x => x.Tag)
                .NotEmpty()
                .WithMessage(MessageHelper.RequiredField)
                .Length(3, 50)
                .WithMessage(MessageHelper.RequiredFieldWithMinAndMax);
        }
    }
}
