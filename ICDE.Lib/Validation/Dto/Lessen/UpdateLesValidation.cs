using FluentValidation;
using ICDE.Lib.Dto.Lessen;

namespace ICDE.Lib.Validation.Dto.Lessen;
internal class UpdateLesValidation : AbstractValidator<UpdateLesDto>
{
    public UpdateLesValidation()
    {
        RuleFor(dto => dto.GroupId).NotEqual(Guid.Empty);
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}