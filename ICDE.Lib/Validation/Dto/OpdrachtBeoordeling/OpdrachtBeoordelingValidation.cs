using FluentValidation;
using ICDE.Lib.Dto.OpdrachtBeoordeling;

namespace ICDE.Lib.Validation.Dto.OpdrachtBeoordeling;
internal class OpdrachtBeoordelingValidation : AbstractValidator<OpdrachtBeoordelingDto>
{
    public OpdrachtBeoordelingValidation()
    {
        RuleFor(dto => dto.Feedback).NotEmpty();
        RuleFor(dto => dto.Cijfer).Must(x => x >= 1 && x <= 11);
    }
}
