using ICDE.Lib.Validation.Leeruitkomsten;

namespace ICDE.Lib.Reports;
internal interface IReportGenerator
{
    Task<List<ValidationResult>> GenerateReport(Guid groupId);
}
