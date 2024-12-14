using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services.Base;

namespace ICDE.Lib.Services.Interfaces;
public interface ILeeruitkomstService : IVersionableServiceBase<LeeruitkomstDto, MaakLeeruitkomstDto, UpdateLeeruitkomstDto>
{
    Task<LeeruitkomstMetEerdereVersiesDto?> HaalOpMetEerdereVersies(Guid leeruitkomstId);
}
