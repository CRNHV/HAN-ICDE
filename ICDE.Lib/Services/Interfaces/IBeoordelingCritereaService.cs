using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Services.Base;

namespace ICDE.Lib.Services.Interfaces;
public interface IBeoordelingCritereaService : IVersionableServiceBase<BeoordelingCritereaDto, MaakBeoordelingCritereaDto, UpdateBeoordelingCritereaDto>
{
    Task<BeoordelingCritereaMetEerdereVersiesDto?> HaalOpMetEerdereVersies(Guid critereaGroupId);
    Task<bool> KoppelLeeruitkomst(Guid beoordelingCritereaGroupId, Guid leeruitkomstGroupId);
    Task<bool> VerwijderLuk(Guid beoordelingCritereaGroupId, Guid leeruitkomstGroupId);
}
