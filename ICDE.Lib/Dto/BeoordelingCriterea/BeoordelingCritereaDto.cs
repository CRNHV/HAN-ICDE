using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Lib.Dto.BeoordelingCriterea;
public class BeoordelingCritereaDto
{
    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public List<LeeruitkomstDto> Leeruitkomsten { get; set; }
}
