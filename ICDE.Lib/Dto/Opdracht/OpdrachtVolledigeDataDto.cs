using ICDE.Lib.Dto.BeoordelingCriterea;

namespace ICDE.Lib.Dto.Opdracht;
public class OpdrachtVolledigeDataDto
{
    public OpdrachtDto Opdracht { get; set; }
    public List<BeoordelingCritereaDto> BeoordelingCritereas { get; set; } = new();
    public List<OpdrachtDto> EerdereVersies { get; set; } = new();
}
