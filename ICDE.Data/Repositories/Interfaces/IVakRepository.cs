using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface IVakRepository : IRepositoryBase<Vak>
{
    public Task<Vak?> GetLatestByGroupId(Guid groupId);
    Task<Vak?> GetFullObjectTreeByGroupId(Guid vakGroupId);
}
