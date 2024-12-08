using ICDE.Lib.Dto.Opdracht;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpdrachtService
{
    Task<OpdrachtDto?> Bekijk(int opdrachtId);
    Task<List<OpdrachtDto>> GetAll();
    Task<List<IngeleverdeOpdrachtDto>> HaalInzendingenOp(int opdrachtId);
    Task<bool> LeverOpdrachtIn(int userId, LeverOpdrachtInDto opdracht);
    Task MaakOpdracht(MaakOpdrachtDto opdracht);
    Task<bool> SlaBeoordelingOp(OpdrachtBeoordelingDto request);
}
