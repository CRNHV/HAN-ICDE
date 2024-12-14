using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface IOpdrachtRepository : IVersionableRepository<Opdracht>
{
    Task<Opdracht?> GetByInzendingId(int inzendingId);
    Task<Opdracht?> GetFullDataByGroupId(Guid groupId);
}
