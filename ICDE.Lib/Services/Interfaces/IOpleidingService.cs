using ICDE.Lib.Dto.Opleidingen;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpleidingService
{
    Task<Guid> Copy(Guid opleidingGroupId);
    Task<OpleidingDto?> Create(CreateOpleiding request);
    Task<bool> Delete(Guid groupId, int versie);
    Task<List<OpleidingDto>> GetAllUnique();
    Task<bool> KoppelVakAanOpleiding(Guid opleidingGroupId, Guid vakGroupId);
    Task<bool> Update(UpdateOpleiding request);
    Task<OpleidingMetEerdereVersiesDto?> ZoekOpleidingMetEerdereVersies(Guid groupId);
}
