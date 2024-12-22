using FluentValidation;
using ICDE.Lib.Dto.Cursus;

namespace ICDE.Lib.Validation.Dto.Cursus;
internal class UpdateCursusValidation : AbstractValidator<UpdateCursusDto>
{
    public UpdateCursusValidation()
    {
        RuleFor(dto => dto.GroupId).NotEqual(Guid.Empty);
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Beschrijving).NotEmpty();
    }
}