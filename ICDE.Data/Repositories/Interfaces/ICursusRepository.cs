using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface ICursusRepository : IRepositoryBase<Cursus>
{
    Task<Cursus?> GetLatestByGroupId(Guid groupId);
    Task<Cursus?> GetFullObjectTreeByGroupId(Guid vakGroupId);
}
