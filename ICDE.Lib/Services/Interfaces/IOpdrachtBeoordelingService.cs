using ICDE.Lib.Dto.OpdrachtBeoordeling;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpdrachtBeoordelingService
{
    Task<List<OpdrachtMetBeoordelingDto>> HaalBeoordelingenOpVoorUser(int? userId);
    Task<bool> SlaBeoordelingOp(OpdrachtBeoordelingDto request);
}
