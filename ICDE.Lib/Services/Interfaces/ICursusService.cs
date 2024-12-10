using ICDE.Lib.Dto.Cursus;

namespace ICDE.Lib.Services.Interfaces;
public interface ICursusService
{
    Task<List<CursusDto>> Allemaal();
    Task<CursusMetPlanningDto?> HaalAlleDataOp(Guid cursusGroupId);
    Task<List<CursusDto>> HaalEerdereVersiesOp(Guid groupId, int exceptId);
    Task<bool> VoegPlanningToeAanCursus(Guid cursusGroupId, int planningId);
}
