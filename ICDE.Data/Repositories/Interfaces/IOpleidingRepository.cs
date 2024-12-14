using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface IOpleidingRepository : IVersionableRepository<Opleiding>
{
    Task<Opleiding?> GetFullObjectTreeByGroupId(Guid groupId);
}
