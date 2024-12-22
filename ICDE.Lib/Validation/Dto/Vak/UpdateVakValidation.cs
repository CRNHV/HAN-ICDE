using FluentValidation;
using ICDE.Lib.Dto.Vak;

namespace ICDE.Lib.Validation.Dto.Vak;
internal class UpdateVakValidation : AbstractValidator<UpdateVakDto>
{
    public UpdateVakValidation()
    {
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.GroupId).NotEqual(Guid.Empty);
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}
