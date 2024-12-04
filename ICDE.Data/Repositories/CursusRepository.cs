using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class CursusRepository : RepositoryBase<Cursus>, ICursusRepository
{
    private readonly AppDbContext _context;

    public CursusRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<Cursus?> GetLatestByGroupId(Guid groupId)
    {
        return await _context.Cursussen
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public async Task<Cursus?> GetFullCursusData(Guid groupId)
    {
        return await _context.Cursussen
            .AsSplitQuery()
            .Include(x => x.Leeruitkomsten)
            .Include(x => x.Planning)
            .ThenInclude(x => x.PlanningItems)
            .ThenInclude(x => x.Les)
            .Include(x => x.Planning)
            .ThenInclude(x => x.PlanningItems)
            .ThenInclude(x => x.Opdracht)
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }
}
