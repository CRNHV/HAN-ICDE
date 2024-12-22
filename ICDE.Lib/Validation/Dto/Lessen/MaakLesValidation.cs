using FluentValidation;
using ICDE.Lib.Dto.Lessen;

namespace ICDE.Lib.Validation.Dto.Lessen;
internal class MaakLesValidation : AbstractValidator<MaakLesDto>
{
    public MaakLesValidation()
    {
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}