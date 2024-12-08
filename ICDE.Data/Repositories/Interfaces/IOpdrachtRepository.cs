using ICDE.Data.Entities;

namespace ICDE.Data.Repositories.Interfaces;
public interface IOpdrachtRepository
{
    Task<Opdracht?> GetLatestByGroupId(Guid groupId);
    Task<List<Opdracht>> HaalAlleOp();
    Task<Opdracht?> GetById(int opdrachtId);
    Task MaakOpdracht(string naam, string beschrijving, bool isToets);
    Task<Opdracht?> GetByInzendingId(int inzendingId);
    Task<List<Opdracht>> GetEarlierVersions(Guid groupId, int exceptId);
    Task<Opdracht?> GetFullDataByGroupId(Guid groupId);
    Task Delete(Guid opdrachtGroupId);
    Task<Opdracht> Update(Opdracht opdracht);
}
