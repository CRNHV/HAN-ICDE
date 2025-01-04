using ICDE.Lib.Validation.Leeruitkomsten;

namespace ICDE.Lib.Reports;
public interface IReportGenerator
{
    Task<List<ValidationResult>> GenerateReport(Guid groupId);
}
