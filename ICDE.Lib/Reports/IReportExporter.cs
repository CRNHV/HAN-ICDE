using ICDE.Lib.Validation.Leeruitkomsten;

namespace ICDE.Lib.Reports;
public interface IReportExporter
{
    byte[] ExportData(List<ValidationResult> validationResults);
}