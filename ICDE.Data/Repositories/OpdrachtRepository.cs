using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
public class OpdrachtRepository : RepositoryBase<Opdracht>, IOpdrachtRepository
{
    private readonly AppDbContext _context;

    public OpdrachtRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<List<Opdracht>> GetList()
    {
        return await _context.Opdrachten
            .GroupBy(l => l.GroupId)
            .Select(g => g.OrderByDescending(l => l.VersieNummer).First())
            .ToListAsync();
    }

    public async Task<List<Opdracht>> GetEarlierVersions(Guid groupId, int exceptId)
    {
        return await _context.Opdrachten
            .Where(x => x.GroupId == groupId && x.Id != exceptId)
            .ToListAsync();
    }

    public async Task<Opdracht?> GetFullDataByGroupId(Guid groupId)
    {
        return await _context.Opdrachten
            .Include(x => x.BeoordelingCritereas)
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public async Task<Opdracht?> GetLatestByGroupId(Guid groupId)
    {
        return await _context.Opdrachten
            .Where(x => x.GroupId == groupId)
            .OrderByDescending(x => x.VersieNummer)
            .FirstOrDefaultAsync();
    }

    public async Task<Opdracht?> GetByInzendingId(int inzendingId)
    {
        return await _context.IngeleverdeOpdrachten
            .Where(x => x.Id == inzendingId)
            .Include(x => x.Opdracht)
            .ThenInclude(x => x.BeoordelingCritereas)
            .ThenInclude(x => x.Leeruitkomsten)
            .Select(x => x.Opdracht)
            .FirstOrDefaultAsync();
    }

    public async Task Delete(Guid opdrachtGroupId)
    {
        var trans = await _context.Database.BeginTransactionAsync();

        try
        {
            var assignments = await _context.Opdrachten.Where(x => x.GroupId == opdrachtGroupId).ToListAsync(); ;
            foreach (var item in assignments)
            {
                var planningItems = _context.PlanningItems.Where(x => x.OpdrachtId == item.Id);
                foreach (var planningItem in planningItems)
                {
                    planningItem.Opdracht = null;
                    planningItem.OpdrachtId = null;
                }

                _context.Opdrachten.Remove(item);
                await _context.SaveChangesAsync();
            }

            await trans.CommitAsync();
        }
        catch (Exception ex)
        {
            await trans.RollbackAsync();
        }
    }
}