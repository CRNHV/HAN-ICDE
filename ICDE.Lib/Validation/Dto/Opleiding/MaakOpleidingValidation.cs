using FluentValidation;
using ICDE.Lib.Dto.Opleidingen;

namespace ICDE.Lib.Validation.Dto.Opleiding;
internal class MaakOpleidingValidation : AbstractValidator<MaakOpleidingDto>
{
    public MaakOpleidingValidation()
    {
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}
