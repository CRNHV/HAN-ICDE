using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Services.Base;

namespace ICDE.Lib.Services.Interfaces;
public interface IPlanningService : ICrudServiceBase<PlanningDto, MaakPlanningDto, UpdatePlanningDto>
{
    Task<PlanningZonderItemsDto?> VoegOpdrachtToe(int planningId, Guid groupId);
    Task<PlanningZonderItemsDto?> VoegLesToe(int planningId, Guid groupId);
    Task<List<LesMetLeeruitkomstenDto>> HaalLessenOpVoorPlanning(int planningId);
    Task<List<PlanningZonderItemsDto>> AlleUnieke();
    Task<PlanningDto?> VoorId(int planningId);
    Task<bool> VerwijderPlanning(int planningId);
}
