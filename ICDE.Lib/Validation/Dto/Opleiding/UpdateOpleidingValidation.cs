using FluentValidation;
using ICDE.Lib.Dto.Opleidingen;

namespace ICDE.Lib.Validation.Dto.Opleiding;
internal class UpdateOpleidingValidation : AbstractValidator<UpdateOpleidingDto>
{
    public UpdateOpleidingValidation()
    {
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.GroupId).NotEqual(Guid.Empty);
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}
