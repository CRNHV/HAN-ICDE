using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Services.Base;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpdrachtService : IVersionableServiceBase<OpdrachtDto, MaakOpdrachtDto, UpdateOpdrachtDto>
{
    Task<bool> VoegCritereaToe(Guid opdrachtGroupId, Guid critereaGroupId);
    Task<OpdrachtVolledigeDataDto?> HaalAlleDataOp(Guid opdrachtGroupId);
    Task<StudentOpdrachtDto?> HaalStudentOpdrachtDataOp(Guid opdrachtGroupId);
}
