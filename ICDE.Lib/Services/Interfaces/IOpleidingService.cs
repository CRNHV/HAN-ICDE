using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Services.Base;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpleidingService : IVersionableServiceBase<OpleidingDto, MaakOpleidingDto, UpdateOpleidingDto>
{
    Task<Guid> Kopie(Guid opleidingGroupId);
    Task<bool> KoppelVakAanOpleiding(Guid opleidingGroupId, Guid vakGroupId);
    Task<OpleidingMetEerdereVersiesDto?> ZoekOpleidingMetEerdereVersies(Guid groupId);
    Task<OpleidingDto?> BekijkVersie(Guid groupId, int versie);
    Task<Guid> MaakKopie(Guid vakGroupId, int vakVersie);
    Task<bool> VerwijderVersie(Guid vakGroupId, int vakVersie);
}
