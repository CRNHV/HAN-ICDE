using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Luk;
public interface ILeeruitkomstRepository : IRepositoryBase<Leeruitkomst>
{
    Task<Leeruitkomst?> GetLatestByGroupId(Guid groupId);
}
