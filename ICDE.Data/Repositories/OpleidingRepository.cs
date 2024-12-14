using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class OpleidingRepository : VersionableRepositoryBase<Opleiding>, IOpleidingRepository
{
    private readonly AppDbContext _context;

    public OpleidingRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<Opleiding?> GetFullObjectTreeByGroupId(Guid groupId)
    {
        var opleiding = await _context.Opleidingen
            .Include(o => o.Vakken)
                .ThenInclude(v => v.Leeruitkomsten)
            .Include(o => o.Vakken)
                .ThenInclude(v => v.Cursussen)
                    .ThenInclude(c => c.Leeruitkomsten)
            .Include(o => o.Vakken)
                .ThenInclude(v => v.Cursussen)
                    .ThenInclude(c => c.Planning)
                        .ThenInclude(p => p.PlanningItems)
                            .ThenInclude(pi => pi.Opdracht)
                                .ThenInclude(o => o.BeoordelingCritereas)
                                    .ThenInclude(bc => bc.Leeruitkomsten)
            .Include(o => o.Vakken)
                .ThenInclude(v => v.Cursussen)
                    .ThenInclude(c => c.Planning)
                        .ThenInclude(p => p.PlanningItems)
                            .ThenInclude(pi => pi.Les)
                                .ThenInclude(l => l.Leeruitkomsten)
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();

        return opleiding;
    }

    public override async Task<Opleiding?> NieuwsteVoorGroepId(Guid groupId)
    {
        return await _context.Opleidingen
            .Include(x => x.Vakken)
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public override async Task<Opleiding?> Versie(Guid groupId, int versieNummer)
    {
        return await _context.Opleidingen
            .Where(x => x.GroupId == groupId && x.VersieNummer == versieNummer)
            .FirstOrDefaultAsync();
    }
}
