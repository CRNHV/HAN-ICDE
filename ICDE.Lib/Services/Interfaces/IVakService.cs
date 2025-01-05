
using ICDE.Lib.Dto.Vak;
using ICDE.Lib.Services.Base;

namespace ICDE.Lib.Services.Interfaces;
public interface IVakService : IVersionableServiceBase<VakDto, MaakVakDto, UpdateVakDto>
{
    Task<VakMetOnderwijsOnderdelenDto?> HaalVolledigeVakDataOp(Guid vakGroupId);
    Task<bool> KoppelCursus(Guid vakGroupId, Guid cursusGroupId);
    Task<bool> KoppelLeeruitkomst(Guid vakGroupId, Guid lukGroupId);
    Task<bool> OntkoppelCursus(Guid vakGroupId, Guid cursusGroupId);
    Task<bool> OntkoppelLeeruitkomst(Guid vakGroupId, Guid lukGroupId);
}
