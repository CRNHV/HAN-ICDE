using FluentValidation;
using ICDE.Lib.Dto.Planning;

namespace ICDE.Lib.Validation.Dto.Planning;
internal class MaakPlanningValidation : AbstractValidator<MaakPlanningDto>
{
    public MaakPlanningValidation()
    {
        RuleFor(dto => dto.Naam).NotEmpty();
    }
}
