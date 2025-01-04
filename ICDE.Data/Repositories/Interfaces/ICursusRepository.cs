using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;

public interface ICursusRepository : IVersionableRepository<Cursus>
{
    Task<List<Cursus>> GetCursussenWithPlanningByPlanningId(int planning);
    Task<Cursus?> GetFullObjectTreeByGroupId(Guid vakGroupId);
}
