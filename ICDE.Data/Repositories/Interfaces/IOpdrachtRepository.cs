using ICDE.Data.Entities.Opdracht;

namespace ICDE.Data.Repositories.Interfaces;
public interface IOpdrachtRepository
{
    Task<List<Opdracht>> HaalAlleOp();
    Task<List<IngeleverdeOpdracht>> HaalInzendingenOp(int opdrachtId);
    Task<Opdracht?> HaalOpdrachtOp(int opdrachtId);
    Task MaakOpdracht(string naam, string beschrijving, bool isToets);
    Task SlaBeoordelingOp(OpdrachtBeoordeling opdrachtBeoordeling);
    Task SlaIngeleverdeOpdrachtOp(IngeleverdeOpdracht ingeleverdeOpdracht);
    Task<IngeleverdeOpdracht> HaalInzendingOp(int inzendingId);
}
