﻿
using ICDE.Lib.Dto.Vak;

namespace ICDE.Lib.Services.Interfaces;
public interface IVakService
{
    Task<Guid> CreateCourse(string naam, string beschrijving);
    Task<bool> Delete(Guid vakGroupId, int vakVersie);
    Task<List<VakDto>> GetAll();
    Task<VakMetOnderwijsOnderdelenDto?> HaalVolledigeVakDataOp(Guid vakGroupId);
    Task<bool> KoppelCursus(Guid vakGroupId, Guid cursusGroupId);
    Task<bool> KoppelLeeruitkomst(Guid vakGroupId, Guid lukGroupId);
    Task<bool> Update(UpdateVakDto request);
}
