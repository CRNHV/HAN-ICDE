using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Dto.Planning;

namespace ICDE.Lib.Dto.Cursus;
public class CursusMetPlanningDto
{
    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public string CursusMateriaal { get; set; }
    public List<LeeruitkomstDto> Leeruitkomsten { get; set; } = new();
    public PlanningDto? Planning { get; set; }
}
