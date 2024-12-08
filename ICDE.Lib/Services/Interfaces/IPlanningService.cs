using ICDE.Lib.Dto.Planning;

namespace ICDE.Lib.Services.Interfaces;
public interface IPlanningService
{
    Task<List<PlanningZonderItemsDto>> GetAll();
    Task<PlanningDto?> GetById(int planningId);
    Task<PlanningZonderItemsDto?> VoegOpdrachtToe(int planningId, Guid groupId);
    Task<PlanningZonderItemsDto?> VoegLesToe(int planningId, Guid groupId);
}
