using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface ILesRepository : IVersionableRepository<Les>
{
    Task<List<Les>> GetLessonsWithLearningGoals(Guid groupId);
}
