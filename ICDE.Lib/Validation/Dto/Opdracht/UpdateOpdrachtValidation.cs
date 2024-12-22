using FluentValidation;
using ICDE.Lib.Dto.Opdracht;

namespace ICDE.Lib.Validation.Dto.Opdracht;
internal class UpdateOpdrachtValidation : AbstractValidator<UpdateOpdrachtDto>
{
    public UpdateOpdrachtValidation()
    {
        RuleFor(dto => dto.GroupId).NotEqual(Guid.Empty);
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}