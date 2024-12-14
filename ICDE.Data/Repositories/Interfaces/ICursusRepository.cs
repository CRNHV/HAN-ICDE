using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface ICursusRepository : IVersionableRepository<Cursus>
{
    Task<Cursus?> GetFullObjectTreeByGroupId(Guid vakGroupId);
}
