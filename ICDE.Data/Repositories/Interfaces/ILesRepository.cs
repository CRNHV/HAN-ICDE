using ICDE.Data.Entities;

namespace ICDE.Data.Repositories.Interfaces;
public interface ILesRepository : IRepositoryBase<Les>
{
    Task<Les?> GetLatestByGroupId(Guid groupId);
    Task<List<Les>> GetLessonsWithLearningGoals(Guid groupId);
}
