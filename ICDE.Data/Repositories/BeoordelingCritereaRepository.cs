using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class BeoordelingCritereaRepository : VersionableRepositoryBase<BeoordelingCriterea>, IBeoordelingCritereaRepository
{
    private readonly AppDbContext _context;
    public BeoordelingCritereaRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<BeoordelingCriterea?> NieuwsteVoorGroepId(Guid groupId)
    {
        return await _context.BeoordelingCritereas
           .Include(x => x.Leeruitkomsten)
           .Where(x => x.GroupId == groupId)
           .OrderByDescending(x => x.VersieNummer)
           .FirstOrDefaultAsync();
    }

    public override async Task<BeoordelingCriterea?> Versie(Guid groupId, int versieNummer)
    {
        return await _context.BeoordelingCritereas
            .Where(x => x.GroupId == groupId && x.VersieNummer == versieNummer)
            .FirstOrDefaultAsync();
    }
}
