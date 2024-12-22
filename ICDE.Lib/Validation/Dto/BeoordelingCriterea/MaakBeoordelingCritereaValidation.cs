using FluentValidation;
using ICDE.Lib.Dto.BeoordelingCriterea;

namespace ICDE.Lib.Validation.Dto.BeoordelingCriterea;
internal class MaakBeoordelingCritereaValidation : AbstractValidator<MaakBeoordelingCritereaDto>
{
    public MaakBeoordelingCritereaValidation()
    {
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}
