using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Lib.Services.Interfaces;
public interface ILeeruitkomstService
{
    Task<LeeruitkomstMetEerdereVersiesDto?> GetEntityWithEarlierVersions(Guid leeruitkomstId);
    Task<List<LeeruitkomstDto>> GetAll();
    Task<LeeruitkomstDto?> MaakLeeruitkomst(MaakLeeruitkomstDto request);
    Task<LeeruitkomstDto?> UpdateLeeruitkomst(LukUpdateDto request);
    Task<LeeruitkomstDto?> GetVersion(Guid groupId, int versieId);
    Task<bool> Delete(Guid groupId, int versieId);
}
