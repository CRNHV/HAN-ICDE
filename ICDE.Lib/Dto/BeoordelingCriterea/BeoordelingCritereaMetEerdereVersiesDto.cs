using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Dto.BeoordelingCriterea;
public class BeoordelingCritereaMetEerdereVersiesDto
{
    public BeoordelingCritereaDto BeoordelingCriterea { get; set; }
    public List<BeoordelingCritereaDto> EerdereVersies { get; set; }
}
