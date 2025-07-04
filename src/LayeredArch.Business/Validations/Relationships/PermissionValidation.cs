using FluentValidation;
using LayeredArch.Business.Models.Relationships;
using LayeredArch.Helper.StaticVariables;

namespace LayeredArch.Business.Validations.Relationships
{
    public class PermissionValidation : AbstractValidator<Permission>
    {
        public PermissionValidation()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage(MessageHelper.RequiredField);

            RuleFor(x => x.ModuleId)
                .NotEmpty()
                .WithMessage(MessageHelper.RequiredField);

            RuleFor(x => x.ModuleId)
                .NotEmpty()
                .WithMessage(MessageHelper.RequiredField);

        }
    }
}
