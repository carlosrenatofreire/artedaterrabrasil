using FluentValidation;
using LayeredArch.Business.Models.Parameters;
using LayeredArch.Helper.StaticVariables;

namespace LayeredArch.Business.Validations.Parameters
{
    public class RoleValidation : AbstractValidator<Role>
    {
        public RoleValidation()
        {

            RuleFor(x => x.Name)
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

            RuleFor(x => x.SupervisorId)
                .NotEmpty()
                .WithMessage(MessageHelper.RequiredField);

        }
    }
}
