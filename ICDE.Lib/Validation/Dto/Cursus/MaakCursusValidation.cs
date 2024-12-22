using FluentValidation;
using ICDE.Lib.Dto.Cursus;

namespace ICDE.Lib.Validation.Dto.Cursus;
internal class MaakCursusValidation : AbstractValidator<MaakCursusDto>
{
    public MaakCursusValidation()
    {
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}