using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface IBeoordelingCritereaRepository : IRepositoryBase<BeoordelingCriterea>
{
    Task<BeoordelingCriterea?> GetFullDataByGroupId(Guid critereaGroupId);
}
