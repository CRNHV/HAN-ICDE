using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface IBeoordelingCritereaRepository : IRepositoryBase<BeoordelingCriterea>
{
    Task<BeoordelingCriterea?> GetFullDataByGroupId(Guid critereaGroupId);
}
