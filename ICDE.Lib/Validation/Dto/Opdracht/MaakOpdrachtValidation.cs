using FluentValidation;
using ICDE.Lib.Dto.Opdracht;

namespace ICDE.Lib.Validation.Dto.Opdracht;
internal class MaakOpdrachtValidation : AbstractValidator<MaakOpdrachtDto>
{
    public MaakOpdrachtValidation()
    {
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}