using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface IOpleidingRepository : IRepositoryBase<Opleiding>
{
    Task<Opleiding?> GetLatestByGroupId(Guid opleidingGroupId);

    Task<Opleiding?> GetFullObjectTreeByGroupId(Guid groupId);
}
