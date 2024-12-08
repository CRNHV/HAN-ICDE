using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;

namespace ICDE.Data.Repositories;
public class OpdrachtBeoordelingRepository : RepositoryBase<OpdrachtBeoordeling>, IOpdrachtBeoordelingRepository
{
    private readonly AppDbContext _context;

    public OpdrachtBeoordelingRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
