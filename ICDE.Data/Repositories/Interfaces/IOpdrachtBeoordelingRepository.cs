using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;

namespace ICDE.Data.Repositories.Interfaces;
public interface IOpdrachtBeoordelingRepository : IRepositoryBase<OpdrachtBeoordeling>
{
    Task<List<OpdrachtBeoordeling>> HaalBeoordelingenOpVoorStudent(int userId);
}
