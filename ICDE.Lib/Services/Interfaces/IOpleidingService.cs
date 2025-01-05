using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Services.Base;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpleidingService : IVersionableServiceBase<OpleidingDto, MaakOpleidingDto, UpdateOpleidingDto>
{
    Task<bool> KoppelVakAanOpleiding(Guid opleidingGroupId, Guid vakGroupId);
    Task<bool> OntkoppelVakVanOpleiding(Guid opleidingGroupId, Guid vakGroupId);
    Task<OpleidingMetEerdereVersiesDto?> ZoekOpleidingMetEerdereVersies(Guid groupId);
}
