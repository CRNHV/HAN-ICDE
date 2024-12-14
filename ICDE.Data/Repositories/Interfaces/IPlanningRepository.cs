using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface IPlanningRepository : ICrudRepository<Planning>
{
    public Task<Planning> CreateCloneOf(int id);
}
