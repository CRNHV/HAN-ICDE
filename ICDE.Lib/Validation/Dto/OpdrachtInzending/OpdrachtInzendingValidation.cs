using FluentValidation;
using ICDE.Lib.Dto.OpdrachtInzending;

namespace ICDE.Lib.Validation.Dto.OpdrachtInzending;
internal class OpdrachtInzendingValidation : AbstractValidator<LeverOpdrachtInDto>
{
    public OpdrachtInzendingValidation()
    {
        RuleFor(dto => dto.OpdrachtId).NotEmpty();
        RuleFor(dto => dto.Naam).NotEmpty();
        RuleFor(dto => dto.Bestand).Must(x => x.Length >= 1);
    }
}
