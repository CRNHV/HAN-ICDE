using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Lib.ViewModels;
public class LeeruitkomstMetEerdereVersies
{
    public LeeruitkomstDto Leeruitkomst { get; set; }
    public List<LeeruitkomstDto> EerdereVersies { get; set; } = new List<LeeruitkomstDto>();
}
