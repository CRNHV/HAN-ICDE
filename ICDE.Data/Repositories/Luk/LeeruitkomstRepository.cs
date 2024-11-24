using ICDE.Data.Entities.OnderwijsOnderdeel;

namespace ICDE.Data.Repositories.Luk;
public class LeeruitkomstRepository : RepositoryBase<Leeruitkomst>, ILeeruitkomstRepository
{
    public LeeruitkomstRepository(AppDbContext context) : base(context)
    {
    }
}
