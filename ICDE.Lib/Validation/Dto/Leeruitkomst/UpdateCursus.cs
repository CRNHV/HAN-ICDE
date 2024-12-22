using FluentValidation;
using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Lib.Validation.Dto.Leeruitkomst;
internal class UpdateLeeruitkomstValidation : AbstractValidator<UpdateLeeruitkomstDto>
{
    public UpdateLeeruitkomstValidation()
    {
        RuleFor(dto => dto.GroupId).NotEqual(Guid.Empty);
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}