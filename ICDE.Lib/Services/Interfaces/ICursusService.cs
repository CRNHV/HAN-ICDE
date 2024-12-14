using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Services.Base;

namespace ICDE.Lib.Services.Interfaces;
public interface ICursusService : IVersionableServiceBase<CursusDto, MaakCursusDto, UpdateCursusDto>
{
    Task<CursusMetPlanningDto?> HaalAlleDataOp(Guid cursusGroupId);
    Task<bool> VoegPlanningToeAanCursus(Guid cursusGroupId, int planningId);
}
