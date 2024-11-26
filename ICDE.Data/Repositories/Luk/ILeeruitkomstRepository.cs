using ICDE.Data.Entities.OnderwijsOnderdeel;

namespace ICDE.Data.Repositories.Luk;
public interface ILeeruitkomstRepository : IRepositoryBase<Leeruitkomst>
{
    Task<Leeruitkomst?> GetLatestByGroupId(Guid groupId);
}
