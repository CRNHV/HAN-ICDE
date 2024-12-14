using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface IVakRepository : IVersionableRepository<Vak>
{
    Task<Vak?> GetFullObjectTreeByGroupId(Guid vakGroupId);
}
