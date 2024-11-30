using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class LesRepository : RepositoryBase<Les>, ILesRepository
{
    private readonly AppDbContext _context;

    public LesRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }

    public override async Task<List<Les>> GetList()
    {
        return await _context.Lessen
            .GroupBy(l => l.GroupId)
            .Select(g => g.OrderByDescending(l => l.VersieNummer).First())
            .ToListAsync();
    }

    public async Task<List<Les>> GetLessonsWithLearningGoals(Guid groupId)
    {
        return await _context.Lessen
            .Include(x => x.Leeruitkomsten)
            .Where(x => x.GroupId == groupId)
            .ToListAsync();
    }

    public async Task<Les?> GetLatestByGroupId(Guid groupId)
    {
        return await _context.Lessen
            .Include(x => x.Leeruitkomsten)
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }
}
