using ICDE.Lib.Dto.Opdracht;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpdrachtService
{
    Task<OpdrachtDto?> Bekijk(Guid opdrachtId);
    Task<List<OpdrachtDto>> GetAll();

    Task MaakOpdracht(MaakOpdrachtDto opdracht);
}
