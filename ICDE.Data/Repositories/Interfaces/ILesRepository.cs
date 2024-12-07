using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface ILesRepository : IRepositoryBase<Les>
{
    Task<Les?> GetLatestByGroupId(Guid groupId);
    Task<List<Les>> GetLessonsWithLearningGoals(Guid groupId);
}
