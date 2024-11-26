using ICDE.Data.Entities.OnderwijsOnderdeel;

namespace ICDE.Data.Repositories.Interfaces;
public interface IVakRepository : IRepositoryBase<Vak>
{
    public Task<Vak?> GetLatestByGroupId(Guid groupId);
}
