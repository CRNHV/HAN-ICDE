using ICDE.Lib.Dto.Opleidingen;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpleidingService
{
    Task<Guid> Copy(Guid opleidingGroupId);
    Task<List<OpleidingDto>> GetAllUnique();
    Task<bool> KoppelVakAanOpleiding(Guid opleidingGroupId, Guid vakGroupId);
    Task<OpleidingMetEerdereVersiesDto?> ZoekOpleidingMetEerdereVersies(Guid groupId);
}
