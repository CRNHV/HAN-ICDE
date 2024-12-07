namespace ICDE.Lib.Dto.Leeruitkomst;
public class LeeruitkomstMetEerdereVersiesDto
{
    public LeeruitkomstDto Leeruitkomst { get; set; }
    public List<LeeruitkomstDto> EerdereVersies { get; set; } = new List<LeeruitkomstDto>();
}
