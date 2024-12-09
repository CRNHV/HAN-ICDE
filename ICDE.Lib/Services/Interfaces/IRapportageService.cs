using ICDE.Lib.Validator;

namespace ICDE.Lib.Services.Interfaces;
public interface IRapportageService
{
    Task<List<ValidationResult>> GenerateReportForOpleiding(Guid opleidingGroupId);
    Task<List<ValidationResult>> GenerateReportForVak(Guid vakGroupId);
    Task<List<ValidationResult>> GenerateReportForCursus(Guid vakGroupId);
}
