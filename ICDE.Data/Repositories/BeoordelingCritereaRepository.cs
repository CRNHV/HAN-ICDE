using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class BeoordelingCritereaRepository : RepositoryBase<BeoordelingCriterea>, IBeoordelingCritereaRepository
{
    private readonly AppDbContext _context;
    public BeoordelingCritereaRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<List<BeoordelingCriterea>> GetList()
    {
        return await _context.BeoordelingCritereas
            .GroupBy(l => l.GroupId)
            .Select(g => g.OrderByDescending(l => l.VersieNummer).First())
            .ToListAsync();
    }
}
