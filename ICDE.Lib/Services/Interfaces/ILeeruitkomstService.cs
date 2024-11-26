using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.ViewModels;

namespace ICDE.Lib.Services.Interfaces;
public interface ILeeruitkomstService
{
    Task<LeeruitkomstMetEerdereVersies> GetEntityWithEarlierVersions(Guid leeruitkomstId);
    Task<List<LeeruitkomstDto>> GetAll();
    Task<LeeruitkomstDto?> MaakLeeruitkomst(MaakLeeruitkomstDto request);
    Task<LeeruitkomstDto?> UpdateLeeruitkomst(LukUpdateDto request);
    Task<LeeruitkomstDto> GetVersion(Guid groupId, int versieId);
}
