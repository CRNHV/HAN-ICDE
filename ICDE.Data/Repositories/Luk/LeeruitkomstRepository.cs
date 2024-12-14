using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories.Luk;
public class LeeruitkomstRepository : VersionableRepositoryBase<Leeruitkomst>, ILeeruitkomstRepository
{
    private readonly AppDbContext _context;

    public LeeruitkomstRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Leeruitkomst?> NieuwsteVoorGroepId(Guid groupId)
    {
        return await _context.leeruitkomstsen
           .Where(x => x.GroupId == groupId)
           .OrderByDescending(x => x.VersieNummer)
           .FirstOrDefaultAsync();
    }

    public override async Task<Leeruitkomst?> Versie(Guid groupId, int versieNummer)
    {
        return await _context.leeruitkomstsen
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }
}
