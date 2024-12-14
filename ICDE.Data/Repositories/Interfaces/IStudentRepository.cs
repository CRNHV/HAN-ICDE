using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface IStudentRepository : ICrudRepository<Student>
{
    Task<int?> ZoekStudentNummerVoorUserId(int userId);
}
