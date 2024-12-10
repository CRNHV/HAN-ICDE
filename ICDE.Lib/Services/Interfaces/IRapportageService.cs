using ICDE.Lib.Validator;

namespace ICDE.Lib.Services.Interfaces;
public interface IRapportageService
{
    Task<List<ValidationResult>> GenereerRapportVoorOpleiding(Guid opleidingGroupId);
    Task<List<ValidationResult>> GenereerRapportVoorVak(Guid vakGroupId);
    Task<List<ValidationResult>> GenereerRapportVoorCursus(Guid vakGroupId);
}
