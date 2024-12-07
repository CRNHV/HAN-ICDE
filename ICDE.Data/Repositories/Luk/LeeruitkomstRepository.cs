using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories.Luk;
public class LeeruitkomstRepository : RepositoryBase<Leeruitkomst>, ILeeruitkomstRepository
{
    private readonly AppDbContext _context;

    public LeeruitkomstRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Leeruitkomst?> GetLatestByGroupId(Guid groupId)
    {
        return await _context.leeruitkomstsen
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public override async Task<List<Leeruitkomst>> GetList()
    {
        return await _context.leeruitkomstsen
            .GroupBy(l => l.GroupId)
            .Select(g => g.OrderByDescending(l => l.VersieNummer).First())
            .ToListAsync();
    }
}
