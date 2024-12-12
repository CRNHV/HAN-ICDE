using ICDE.Lib.Dto.Opleidingen;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpleidingService
{
    Task<Guid> Kopie(Guid opleidingGroupId);
    Task<OpleidingDto?> Maak(CreateOpleiding request);
    Task<bool> Verwijder(Guid groupId, int versie);
    Task<List<OpleidingDto>> HaalUniekeOp();
    Task<bool> KoppelVakAanOpleiding(Guid opleidingGroupId, Guid vakGroupId);
    Task<bool> Update(UpdateOpleiding request);
    Task<OpleidingMetEerdereVersiesDto?> ZoekOpleidingMetEerdereVersies(Guid groupId);
    Task<OpleidingDto?> BekijkVersie(Guid groupId, int versie);
    Task<Guid> MaakKopie(Guid vakGroupId, int vakVersie);
    Task<bool> VerwijderVersie(Guid vakGroupId, int vakVersie);
}
