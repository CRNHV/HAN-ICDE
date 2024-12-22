using FluentValidation;
using ICDE.Lib.Dto.Planning;

namespace ICDE.Lib.Validation.Dto.Planning;
internal class UpdatePlanningValidation : AbstractValidator<UpdatePlanningDto>
{
    public UpdatePlanningValidation()
    {
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.GroupId).NotEqual(Guid.Empty);
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}
