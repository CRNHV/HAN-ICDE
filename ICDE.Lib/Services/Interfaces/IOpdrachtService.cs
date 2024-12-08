using ICDE.Lib.Dto.Opdracht;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpdrachtService
{
    Task<OpdrachtDto?> Bekijk(Guid opdrachtId);
    Task<List<OpdrachtDto>> GetAll();
    Task<OpdrachtVolledigeDataDto?> GetFullDataByGroupId(Guid opdrachtGroupId);
    Task MaakOpdracht(MaakOpdrachtDto opdracht);
    Task UpdateOpdracht(OpdrachtUpdateDto request);
    Task VerwijderOpdracht(Guid opdrachtGroupId);
}
