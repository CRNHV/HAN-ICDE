using ICDE.Lib.Validator;

namespace ICDE.Lib.Reports;
internal interface IReportGenerator
{
    Task<List<ValidationResult>> GenerateReport(Guid groupId);
}
