using ICDE.Data.Entities;

namespace ICDE.Data.Repositories.Interfaces;
public interface IVakRepository : IRepositoryBase<Vak>
{
    public Task<Vak?> GetLatestByGroupId(Guid groupId);
}
