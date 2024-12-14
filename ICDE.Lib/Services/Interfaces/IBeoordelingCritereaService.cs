using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Services.Base;

namespace ICDE.Lib.Services.Interfaces;
public interface IBeoordelingCritereaService : IVersionableServiceBase<BeoordelingCritereaDto, MaakBeoordelingCritereaDto, UpdateBeoordelingCritereaDto>
{
    Task<BeoordelingCritereaMetEerdereVersiesDto?> HaalOpMetEerdereVersies(Guid critereaGroupId);
}
