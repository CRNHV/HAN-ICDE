using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Services.Interfaces;
using ICDE.Lib.Validator;
using ICDE.Lib.Validator.Interfaces;

namespace ICDE.Lib.Services;
internal class RapportageService : IRapportageService
{
    private readonly IValidationManager _validationManager;
    private readonly IOpleidingRepository _opleidingRepository;
    private readonly IVakRepository _vakRepository;
    private readonly ICursusRepository _cursusRepository;

    public RapportageService(IValidationManager validationManager, IOpleidingRepository opleidingRepository, IVakRepository vakRepository, ICursusRepository cursusRepository)
    {
        _validationManager = validationManager;
        _opleidingRepository = opleidingRepository;
        _vakRepository = vakRepository;
        _cursusRepository = cursusRepository;
    }

    public async Task<List<ValidationResult>> GenerateReportForOpleiding(Guid opleidingGroupId)
    {
        var opleiding = await _opleidingRepository.GetFullObjectTreeByGroupId(opleidingGroupId);
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

    public async Task<List<ValidationResult>> GenerateReportForVak(Guid vakGroupId)
    {
        var vak = await _vakRepository.GetFullObjectTreeByGroupId(vakGroupId);
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

    public async Task<List<ValidationResult>> GenerateReportForCursus(Guid vakGroupId)
    {
        var cursus = await _cursusRepository.GetFullObjectTreeByGroupId(vakGroupId);
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
