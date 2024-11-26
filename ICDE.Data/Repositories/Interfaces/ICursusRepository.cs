using ICDE.Data.Entities.OnderwijsOnderdeel;

namespace ICDE.Data.Repositories.Interfaces;
public interface ICursusRepository : IRepositoryBase<Cursus>
{
    Task<Cursus?> GetLatestByGroupId(Guid groupId);
}
