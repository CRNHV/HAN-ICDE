using System.Collections.Generic;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Dto.Opdracht;

namespace ICDE.Web.Models.Opdrachten;

public class AuteurBekijkOpdrachtViewModel
{
    public OpdrachtVolledigeDataDto Opdracht { get; set; }
    public List<BeoordelingCritereaDto> BeoordelingCritereas { get; set; } = new();
}
