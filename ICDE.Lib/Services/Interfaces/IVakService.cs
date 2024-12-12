
using ICDE.Lib.Dto.Vak;

namespace ICDE.Lib.Services.Interfaces;
public interface IVakService
{
    Task<Guid> MaakVak(string naam, string beschrijving);
    Task<bool> VerwijderVersie(Guid vakGroupId, int vakVersie);
    Task<List<VakDto>> Allemaal();
    Task<VakMetOnderwijsOnderdelenDto?> HaalVolledigeVakDataOp(Guid vakGroupId);
    Task<bool> KoppelCursus(Guid vakGroupId, Guid cursusGroupId);
    Task<bool> KoppelLeeruitkomst(Guid vakGroupId, Guid lukGroupId);
    Task<bool> Update(UpdateVakDto request);
    Task<VakDto?> BekijkVersie(Guid vakGroupId, int vakVersie);
    Task<Guid> MaakKopie(Guid vakGroupId, int vakVersie);
}
