using ICDE.Lib.Dto.Opdracht;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpdrachtService
{
    Task<OpdrachtDto?> Bekijk(int opdrachtId);
    Task<List<OpdrachtDto>> HaalAlleOp();
    Task<List<IngeleverdeOpdrachtDto>> HaalInzendingenOp(int opdrachtId);
    Task LeverOpdrachtIn(int userId, LeverOpdrachtInDto opdracht);
    Task MaakOpdracht(MaakOpdrachtDto opdracht);
    Task SlaBeoordelingOp(OpdrachtBeoordelingDto request);
}
