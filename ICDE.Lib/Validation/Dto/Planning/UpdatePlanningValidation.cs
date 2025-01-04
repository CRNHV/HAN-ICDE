using FluentValidation;
using ICDE.Lib.Dto.Planning;

namespace ICDE.Lib.Validation.Dto.Planning;
internal class UpdatePlanningValidation : AbstractValidator<UpdatePlanningDto>
{
    public UpdatePlanningValidation()
    {
        RuleFor(dto => dto.Name).NotEmpty();
    }
}
