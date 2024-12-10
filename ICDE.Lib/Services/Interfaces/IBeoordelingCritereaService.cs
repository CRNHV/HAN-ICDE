using ICDE.Lib.Dto.BeoordelingCriterea;

namespace ICDE.Lib.Services.Interfaces;
public interface IBeoordelingCritereaService
{
    Task<List<BeoordelingCritereaDto>> Unieke();
    Task<BeoordelingCritereaMetEerdereVersiesDto?> HaalOpMetEerdereVersies(Guid critereaGroupId);
}
