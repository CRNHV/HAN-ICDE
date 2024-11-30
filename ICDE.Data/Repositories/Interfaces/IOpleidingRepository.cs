using ICDE.Data.Entities;

namespace ICDE.Data.Repositories.Interfaces;
public interface IOpleidingRepository : IRepositoryBase<Opleiding>
{
    Task<Opleiding?> GetLatestByGroupId(Guid opleidingGroupId);
}
