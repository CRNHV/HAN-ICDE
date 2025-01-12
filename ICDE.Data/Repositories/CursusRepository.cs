using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class CursusRepository : VersionableRepositoryBase<Cursus>, ICursusRepository
{
    private readonly AppDbContext _context;

    public CursusRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<Cursus?> GetFullObjectTreeByGroupId(Guid vakGroupId)
    {
        return await _context.Cursussen
                    .Include(x => x.Leeruitkomsten)
                    .Include(c => c.Planning)
                        .ThenInclude(p => p.PlanningItems)
                            .ThenInclude(pi => pi.Opdracht)
                                .ThenInclude(o => o.BeoordelingCritereas)
                                    .ThenInclude(bc => bc.Leeruitkomsten)
                    .Include(c => c.Planning)
                        .ThenInclude(p => p.PlanningItems)
                            .ThenInclude(pi => pi.Les)
                                .ThenInclude(l => l.Leeruitkomsten)
            .Where(x => x.GroupId == vakGroupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public override async Task<Cursus?> Versie(Guid groupId, int versieNummer)
    {
        return await _context.Cursussen
            .Include(c => c.Planning)
                        .ThenInclude(p => p.PlanningItems)
                            .ThenInclude(pi => pi.Opdracht)
                                .ThenInclude(o => o.BeoordelingCritereas)
                                    .ThenInclude(bc => bc.Leeruitkomsten)
                    .Include(c => c.Planning)
                        .ThenInclude(p => p.PlanningItems)
                            .ThenInclude(pi => pi.Les)
                                .ThenInclude(l => l.Leeruitkomsten)
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public override async Task<Cursus?> NieuwsteVoorGroepId(Guid groupId)
    {
        return await _context.Cursussen
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Cursus>> GetCursussenWithPlanningByPlanningId(int planningId)
    {
        return await _context.Cursussen
            .Include(c => c.Planning)
            .Where(c => c.Planning != null && c.Planning.Id == planningId)
            .ToListAsync();
    }
}
