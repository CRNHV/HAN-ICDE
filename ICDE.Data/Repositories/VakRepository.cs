using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class VakRepository : RepositoryBase<Vak>, IVakRepository
{
    private readonly AppDbContext _context;

    public VakRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<List<Vak>> GetList()
    {
        return await _context.Vakken
            .Include(x => x.Cursussen)
            .Include(x => x.Leeruitkomsten)
            .GroupBy(l => l.GroupId)
            .Select(g => g.OrderByDescending(l => l.VersieNummer).First())
            .ToListAsync();
    }

    public async Task<Vak?> GetLatestByGroupId(Guid groupId)
    {
        return await _context.Vakken
            .Include(x => x.Cursussen)
            .Include(x => x.Leeruitkomsten)
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public async Task<Vak?> GetFullObjectTreeByGroupId(Guid vakGroupId)
    {
        return await _context.Vakken
                .Include(v => v.Leeruitkomsten)
                .Include(v => v.Cursussen)
                    .ThenInclude(c => c.Leeruitkomsten)
                .Include(v => v.Cursussen)
                    .ThenInclude(c => c.Planning)
                        .ThenInclude(p => p.PlanningItems)
                            .ThenInclude(pi => pi.Opdracht)
                                .ThenInclude(o => o.BeoordelingCritereas)
                                    .ThenInclude(bc => bc.Leeruitkomsten)
                .Include(v => v.Cursussen)
                    .ThenInclude(c => c.Planning)
                        .ThenInclude(p => p.PlanningItems)
                            .ThenInclude(pi => pi.Les)
                                .ThenInclude(l => l.Leeruitkomsten)
            .Where(x => x.GroupId == vakGroupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }
}
