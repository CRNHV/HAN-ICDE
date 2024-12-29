using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Validation.Leeruitkomsten;

namespace ICDE.Lib.Reports;
internal class CursusReportGenerator : IReportGenerator
{
    private readonly ICursusRepository _cursusRepository;
    private readonly IValidationManager _validationManager;

    public CursusReportGenerator(IValidationManager validationManager, ICursusRepository cursusRepository)
    {
        _cursusRepository = cursusRepository;
        _validationManager = validationManager;
    }

    public async Task<List<ValidationResult>> GenerateReport(Guid groupId)
    {
        var cursus = await _cursusRepository.GetFullObjectTreeByGroupId(groupId);
        if (cursus is null)
        {
            return new List<ValidationResult>();
        }

        _validationManager.RegisterValidator(new CursusValidator(cursus));
        if (cursus.Planning != null)
            _validationManager.RegisterValidator(new PlanningValidator(cursus.Planning));

        var validationResult = _validationManager.ValidateAll();
        return validationResult;
    }
}
