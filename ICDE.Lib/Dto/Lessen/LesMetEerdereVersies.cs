using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Lib.Dto.Lessen;
public class LesMetEerdereVersies
{
    public LesDto Les { get; set; }
    public List<LesDto> LesList { get; set; } = new List<LesDto>();
    public List<LeeruitkomstDto> LesLeeruitkomsten { get; set; } = new();
}
