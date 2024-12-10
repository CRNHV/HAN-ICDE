using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Dto.Planning;

namespace ICDE.Lib.Services.Interfaces;
public interface IPlanningService
{
    Task<List<PlanningZonderItemsDto>> Allemaal();
    Task<PlanningDto?> ZoekMetId(int planningId);
    Task<PlanningZonderItemsDto?> VoegOpdrachtToe(int planningId, Guid groupId);
    Task<PlanningZonderItemsDto?> VoegLesToe(int planningId, Guid groupId);
    Task<List<LesMetLeeruitkomstenDto>> HaalLessenOpVoorPlanning(int planningId);
}
