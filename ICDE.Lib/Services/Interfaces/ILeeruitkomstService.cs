using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Lib.Services.Interfaces;
public interface ILeeruitkomstService
{
    Task<LeeruitkomstMetEerdereVersiesDto?> HaalOpMetEerdereVersies(Guid leeruitkomstId);
    Task<List<LeeruitkomstDto>> Allemaal();
    Task<LeeruitkomstDto?> MaakLeeruitkomst(MaakLeeruitkomstDto request);
    Task<LeeruitkomstDto?> UpdateLeeruitkomst(LukUpdateDto request);
    Task<LeeruitkomstDto?> HaalVersieOp(Guid groupId, int versieId);
    Task<bool> Verwijder(Guid groupId, int versieId);
    Task<Guid> MaakKopieVanVersie(Guid groupId, int versieId);
}
