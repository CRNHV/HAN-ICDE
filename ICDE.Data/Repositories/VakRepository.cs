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
}
