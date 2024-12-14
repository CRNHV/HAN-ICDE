namespace ICDE.Lib.Dto.Opleidingen;
public class OpleidingMetEerdereVersiesDto
{
    public OpleidingMetVakkenDto OpleidingDto { get; set; } = new();
    public List<OpleidingDto> EerdereVersies { get; set; } = new();

}
