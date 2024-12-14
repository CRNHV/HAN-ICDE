using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class PlanningRepository : CrudRepositoryBase<Planning>, IPlanningRepository
{
    private readonly AppDbContext _context;

    public PlanningRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Planning> CreateCloneOf(int id)
    {
        var planning = await _context.Plannings
            .Include(x => x.PlanningItems)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        var newPlanning = (Planning)planning.Clone();
        await _context.Plannings.AddAsync(newPlanning);
        await _context.SaveChangesAsync();
        return newPlanning;
    }

    //public override async Task<Planning?> Get(int id)
    //{
    //    return await _context.Plannings
    //        .Include(x => x.PlanningItems)
    //        .ThenInclude(x => x.Les)
    //        .ThenInclude(x => x.Leeruitkomsten)
    //        .Include(x => x.PlanningItems)
    //        .ThenInclude(x => x.Opdracht)
    //        .Where(x => x.Id == id)
    //        .FirstOrDefaultAsync();
    //}
}
