using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Validation.Leeruitkomsten;

namespace ICDE.Lib.Reports;
internal class OpleidingReportGenerator : IReportGenerator
{
    private readonly IValidationManager _validationManager;
    private readonly IOpleidingRepository _opleidingRepository;

    public OpleidingReportGenerator(IValidationManager validationManager, IOpleidingRepository opleidingRepository)
    {
        _validationManager = validationManager;
        _opleidingRepository = opleidingRepository;
    }

    public async Task<List<ValidationResult>> GenerateReport(Guid groupId)
    {
        var opleiding = await _opleidingRepository.GetFullObjectTreeByGroupId(groupId);
        if (opleiding is null)
        {
            return new List<ValidationResult>();
        }

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
        return validationResult;
    }
}
