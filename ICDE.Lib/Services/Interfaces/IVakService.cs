
using ICDE.Lib.Dto.Vak;

namespace ICDE.Lib.Services.Interfaces;
public interface IVakService
{
    Task<Guid> CreateCourse(string naam, string beschrijving);
    Task<List<VakDto>> GetAll();
    Task<VakMetOnderwijsOnderdelenDto> HaalVolledigeVakDataOp(Guid vakGroupId);
    Task KoppelCursus(Guid vakGroupId, Guid cursusGroupId);
    Task KoppelLeeruitkomst(Guid vakGroupId, Guid lukGroupId);
}
