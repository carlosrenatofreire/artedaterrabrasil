using ArteDaTerraBrasil.Business.Models.Parameters;
using ArteDaTerraBrasil.Helper.StaticVariables;
using FluentValidation;

namespace ArteDaTerraBrasil.Business.Validations.Parameters
{
    public class ModuleValidation : AbstractValidator<Module>
    {
        public ModuleValidation()
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
        }
    }
}
