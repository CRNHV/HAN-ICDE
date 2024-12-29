using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Validation.Leeruitkomsten;

namespace ICDE.Lib.Reports;
internal class VakReportGenerator : IReportGenerator
{
    private readonly IValidationManager _validationManager;
    private readonly IVakRepository _vakRepository;

    public VakReportGenerator(IValidationManager validationManager, IVakRepository vakRepository)
    {
        _vakRepository = vakRepository;
        _validationManager = validationManager;
    }

    public async Task<List<ValidationResult>> GenerateReport(Guid groupId)
    {
        var vak = await _vakRepository.GetFullObjectTreeByGroupId(groupId);
        if (vak is null)
        {
            return new List<ValidationResult>();
        }

        _validationManager.RegisterValidator(new VakValidator(vak));
        foreach (var cursus in vak.Cursussen)
        {
            _validationManager.RegisterValidator(new CursusValidator(cursus));
            if (cursus.Planning != null)
                _validationManager.RegisterValidator(new PlanningValidator(cursus.Planning));
        }

        var validationResult = _validationManager.ValidateAll();
        return validationResult;
    }
}
