using ICDE.Lib.Dto.Planning;

namespace ICDE.Lib.Dto.Opleidingen;
public class OpleidingMetEerdereVersiesDto
{
    public OpleidingMetVakkenDto OpleidingDto { get; set; }
    public List<OpleidingDto> EerdereVersies { get; set; }

}
