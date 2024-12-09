using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface IOpdrachtRepository : IRepositoryBase<Opdracht>
{
    Task<Opdracht?> GetLatestByGroupId(Guid groupId);
    Task<Opdracht?> GetByInzendingId(int inzendingId);
    Task<List<Opdracht>> GetEarlierVersions(Guid groupId, int exceptId);
    Task<Opdracht?> GetFullDataByGroupId(Guid groupId);
    Task Delete(Guid opdrachtGroupId);
}
