using ICDE.Lib.Validator;

namespace ICDE.Lib.Services.Interfaces;
public interface IRapportageService
{
    Task<List<ValidationResult>> GenereerRapportage(string type, Guid groupId);
    Task<RapportageExportDto?> ExporteerRapportage(string type, Guid groupId);
}
