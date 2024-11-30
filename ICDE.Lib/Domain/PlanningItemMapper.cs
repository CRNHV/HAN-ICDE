using ICDE.Data.Entities;
using ICDE.Lib.Dto.Planning;

namespace ICDE.Lib.Domain;
internal static class PlanningItemMapper
{
    internal static PlanningItemDto MapPlanningItemToDto(PlanningItem planningItem)
    {
        var les = planningItem.Les;
        var opdracht = planningItem.Opdracht;

        return new PlanningItemDto()
        {
            Index = planningItem.Index,
            PlanningItemNaam = les != null ? $"Les: {les.Naam}" : $"Opdracht: {opdracht.Naam}"
        };
    }
}
