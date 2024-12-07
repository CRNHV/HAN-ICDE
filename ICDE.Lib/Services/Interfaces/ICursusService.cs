using ICDE.Lib.Dto.Cursus;

namespace ICDE.Lib.Services.Interfaces;
public interface ICursusService
{
    Task<List<CursusDto>> GetAll();
    Task<CursusMetPlanningDto> GetFullCursusByGroupId(Guid cursusGroupId);
    Task<List<CursusDto>> GetEarlierVersionsByGroupId(Guid groupId, int exceptId);
    Task VoegPlanningToeAanCursus(Guid cursusGroupId, int planningId);
}
