using ICDE.Lib.Validation.Leeruitkomsten;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ICDE.Lib.Reports;
public interface IReportExporter
{
    byte[] ExportData(List<ValidationResult> validationResults);
}