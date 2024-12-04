using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class OpleidingRepository : RepositoryBase<Opleiding>, IOpleidingRepository
{
    private readonly AppDbContext _context;

    public OpleidingRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<Opleiding?> GetLatestByGroupId(Guid opleidingGroupId)
    {
        return await _context.Opleidingen
            .Include(x => x.Vakken)
            .Where(x => x.GroupId == opleidingGroupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public override async Task<List<Opleiding>> GetList()
    {
        return await _context.Opleidingen
            .GroupBy(l => l.GroupId)
            .Select(g => g.OrderByDescending(l => l.VersieNummer).First())
            .ToListAsync();
    }
}
