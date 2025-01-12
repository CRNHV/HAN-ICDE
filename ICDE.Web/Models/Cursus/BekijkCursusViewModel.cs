using System.Collections.Generic;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Web.Models.Cursus;

public class BekijkCursusViewModel
{
    public CursusMetPlanningDto? Cursus { get; set; }
    public List<CursusDto> EerderVersies { get; set; } = new();
    public List<LeeruitkomstDto> Leeruitkomsten { get; set; } = new();
}
