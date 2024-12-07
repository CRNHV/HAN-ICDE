using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Services.Interfaces;
using ICDE.Lib.Validator;
using ICDE.Lib.Validator.Interfaces;

namespace ICDE.Lib.Services;
internal class RapportageService : IRapportageService
{
    private readonly IValidationManager _validationManager;
    private readonly IOpleidingRepository _opleidingRepository;

    public RapportageService(IValidationManager validationManager, IOpleidingRepository opleidingRepository)
    {
        _validationManager = validationManager;
        _opleidingRepository = opleidingRepository;
    }

    public async Task<bool> ValidateOpleiding(Guid opleidingGroupId)
    {
        var opleiding = await _opleidingRepository.GetFullObjectTreeByGroupId(opleidingGroupId);

        foreach (var vak in opleiding.Vakken)
        {
            _validationManager.RegisterValidator(new VakValidator(vak));
            foreach (var cursus in opleiding.Vakken.SelectMany(x => x.Cursussen).ToList())
            {
                _validationManager.RegisterValidator(new CursusValidator(cursus));
                if (cursus.Planning != null)
                    _validationManager.RegisterValidator(new PlanningValidator(cursus.Planning));
            }
        }

        var validationResult = _validationManager.ValidateAll();
        return validationResult.All(x => x.Success);
    }
}
