using ICDE.Data.Entities;

namespace ICDE.Data.Repositories.Luk;
public interface ILeeruitkomstRepository : IRepositoryBase<Leeruitkomst>
{
    Task<Leeruitkomst?> GetLatestByGroupId(Guid groupId);
}
