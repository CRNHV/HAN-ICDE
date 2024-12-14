using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class VakRepository : VersionableRepositoryBase<Vak>, IVakRepository
{
    private readonly AppDbContext _context;

    public VakRepository(AppDbContext context) : base(context)
    {
        _context = context;
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

    public override async Task<Vak?> Versie(Guid groupId, int versieNummer)
    {
        return await _context.Vakken
            .Include(x => x.Cursussen)
            .Include(x => x.Leeruitkomsten)
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public override async Task<Vak?> NieuwsteVoorGroepId(Guid groupId)
    {
        return await _context.Vakken
            .Include(x => x.Cursussen)
            .Include(x => x.Leeruitkomsten)
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }
}
