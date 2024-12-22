using FluentValidation;
using ICDE.Lib.Dto.BeoordelingCriterea;

namespace ICDE.Lib.Validation.Dto.BeoordelingCriterea;
internal class UpdateBeoordelingCritereaValidation : AbstractValidator<UpdateBeoordelingCritereaDto>
{
    public UpdateBeoordelingCritereaValidation()
    {
        RuleFor(dto => dto.GroupId).NotEqual(Guid.Empty);
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}