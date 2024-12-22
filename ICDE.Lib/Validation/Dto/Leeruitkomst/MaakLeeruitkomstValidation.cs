using FluentValidation;
using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Lib.Validation.Dto.Leeruitkomst;
internal class MaakLeeruitkomstValidation : AbstractValidator<MaakLeeruitkomstDto>
{
    public MaakLeeruitkomstValidation()
    {
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}