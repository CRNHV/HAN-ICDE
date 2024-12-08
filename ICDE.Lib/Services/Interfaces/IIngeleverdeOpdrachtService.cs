using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Dto.OpdrachtInzending;

namespace ICDE.Lib.Services.Interfaces;
public interface IIngeleverdeOpdrachtService
{
    Task<OpdrachtInzendingDto?> HaalInzendingDataOp(int inzendingId);
    Task<List<IngeleverdeOpdrachtDto>> HaalInzendingenOp(Guid opdrachtId);
    Task<bool> LeverOpdrachtIn(int userId, LeverOpdrachtInDto opdracht);
    Task<bool> SlaBeoordelingOp(OpdrachtBeoordelingDto request);
}
