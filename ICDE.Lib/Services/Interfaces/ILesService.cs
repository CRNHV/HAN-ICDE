using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Services.Base;

namespace ICDE.Lib.Services.Interfaces;
public interface ILesService : IVersionableServiceBase<LesDto, MaakLesDto, UpdateLesDto>
{
    Task<LesMetEerdereVersies?> HaalLessenOpMetEerdereVersies(Guid groupId);
    Task<bool> KoppelLeeruitkomst(Guid lesGroupId, Guid lukGroupId);
    Task<bool> OntkoppelLukAanLes(Guid lesGroupId, Guid lukGroupId);
}
