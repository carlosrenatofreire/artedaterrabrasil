using ArteDaTerraBrasil.Business.Models.Relationships;
using ArteDaTerraBrasil.Helper.StaticVariables;
using FluentValidation;

namespace ArteDaTerraBrasil.Business.Validations.Relationships
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
