using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class IngeleverdeOpdrachtRepository : RepositoryBase<IngeleverdeOpdracht>, IIngeleverdeOpdrachtRepository
{
    private readonly AppDbContext _context;

    public IngeleverdeOpdrachtRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IngeleverdeOpdracht?> Get(int id)
    {
        return await _context.IngeleverdeOpdrachten
            .Include(x => x.Beoordelingen)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
}
