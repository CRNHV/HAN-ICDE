using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class LesRepository : VersionableRepositoryBase<Les>, ILesRepository
{
    private readonly AppDbContext _context;

    public LesRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<List<Les>> GetLessonsWithLearningGoals(Guid groupId)
    {
        return await _context.Lessen
            .Include(x => x.Leeruitkomsten)
            .Where(x => x.GroupId == groupId)
            .ToListAsync();
    }

    public override async Task<Les?> Versie(Guid groupId, int versieNummer)
    {
        return await _context.Lessen
            .Include(x => x.Leeruitkomsten)
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public override async Task<Les?> NieuwsteVoorGroepId(Guid groupId)
    {
        return await _context.Lessen
           .Include(x => x.Leeruitkomsten)
           .Where(x => x.GroupId == groupId)
           .OrderByDescending(x => x.VersieNummer)
           .FirstOrDefaultAsync();
    }
}
