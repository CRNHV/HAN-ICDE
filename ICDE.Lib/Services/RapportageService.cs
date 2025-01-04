using ICDE.Lib.Dto.Rapportage;
using ICDE.Lib.IO;
using ICDE.Lib.Reports;
using ICDE.Lib.Services.Interfaces;
using ICDE.Lib.Validation.Leeruitkomsten;
using Microsoft.Extensions.DependencyInjection;

namespace ICDE.Lib.Services;
internal class RapportageService : IRapportageService
{
    private readonly IReportGenerator _cursusReportGenerator;
    private readonly IReportGenerator _vakReportGenerator;
    private readonly IReportGenerator _opleidingReportGenerator;
    private readonly IReportExporter _exporter;
    private readonly IFileManager _fileManager;

    public RapportageService(
        [FromKeyedServices("CursusReportGenerator")] IReportGenerator cursusReportGenerator,
        [FromKeyedServices("VakReportGenerator")] IReportGenerator vakReportGenerator,
        [FromKeyedServices("OpleidingReportGenerator")] IReportGenerator opleidingReportGenerator,
        IReportExporter exporter,
        IFileManager fileManager)
    {
        _cursusReportGenerator = cursusReportGenerator;
        _vakReportGenerator = vakReportGenerator;
        _opleidingReportGenerator = opleidingReportGenerator;
        _exporter = exporter;
        _fileManager = fileManager;
    }

    public Task<List<ValidationResult>> GenereerRapportage(string type, Guid groupId)
    {
        return type switch
        {
            "opleiding" => _opleidingReportGenerator.GenerateReport(groupId),
            "vak" => _vakReportGenerator.GenerateReport(groupId),
            "cursus" => _cursusReportGenerator.GenerateReport(groupId),
            _ => throw new NotImplementedException(),
        };
    }

    public async Task<RapportageExportDto?> ExporteerRapportage(string type, Guid groupId)
    {
        var rapportage = await GenereerRapportage(type, groupId);
        if (rapportage.Count == 0)
            return null;

        var exportBytes = _exporter.ExportData(rapportage);
        string bestandsNaam = $"{type} rapportage - {DateTime.Now.ToShortTimeString()}";
        var rapportagePad = await _fileManager.SlaRapportageOp(bestandsNaam, exportBytes);
        if (string.IsNullOrEmpty(rapportagePad))
        {
            return null;
        }

        return new RapportageExportDto()
        {
            Type = "pdf",
            Bytes = exportBytes
        };
    }
}

