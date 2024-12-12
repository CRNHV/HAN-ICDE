using ICDE.Lib.Dto.Opdracht;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpdrachtService
{
    Task<bool> VoegCritereaToe(Guid opdrachtGroupId, Guid critereaGroupId);
    Task<OpdrachtDto?> Bekijk(Guid opdrachtId);
    Task<List<OpdrachtDto>> Allemaal();
    Task<OpdrachtVolledigeDataDto?> HaalAlleDataOp(Guid opdrachtGroupId);
    Task MaakOpdracht(MaakOpdrachtDto opdracht);
    Task UpdateOpdracht(OpdrachtUpdateDto request);
    Task VerwijderOpdracht(Guid opdrachtGroupId);
    Task<OpdrachtDto> HaalVersieDataOp(Guid opdrachtGroupId, int versie);
    Task<Guid> MaakKopieVanVersie(Guid opdrachtGroupId, int versie);
}
