using ICDE.Lib.Dto.Vak;

namespace ICDE.Lib.Dto.Planning;
public class OpleidingMetVakkenDto
{
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public Guid GroupId { get; set; }
    public int VersieNummer { get; set; }
    public List<VakDto> Vakken { get; set; } = new();
}
