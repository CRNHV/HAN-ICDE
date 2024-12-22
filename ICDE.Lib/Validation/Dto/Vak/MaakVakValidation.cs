using FluentValidation;
using ICDE.Lib.Dto.Vak;

namespace ICDE.Lib.Validation.Dto.Vak;
internal class MaakVakValidation : AbstractValidator<MaakVakDto>
{
    public MaakVakValidation()
    {
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}
