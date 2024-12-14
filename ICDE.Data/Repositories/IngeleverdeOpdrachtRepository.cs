using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;

namespace ICDE.Data.Repositories;
internal class IngeleverdeOpdrachtRepository : CrudRepositoryBase<IngeleverdeOpdracht>, IIngeleverdeOpdrachtRepository
{
    private readonly AppDbContext _context;

    public IngeleverdeOpdrachtRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
