using System.Collections.Generic;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Web.Models.BeoordelingCriterea;

public class BekijkBeoorderlingCritereaViewModel
{
    public BeoordelingCritereaDto BeoordelingCriterea { get; set; }
    public List<BeoordelingCritereaDto> EerdereVersies { get; set; }
    public List<LeeruitkomstDto> Leeruitkomsten { get; set; }
}
