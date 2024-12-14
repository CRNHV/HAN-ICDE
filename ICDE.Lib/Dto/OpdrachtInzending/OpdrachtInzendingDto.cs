using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Dto.OpdrachtBeoordeling;

namespace ICDE.Lib.Dto.OpdrachtInzending;
public class OpdrachtInzendingDto
{
    public IngeleverdeOpdrachtDto IngeleverdeOpdracht { get; set; }
    public List<OpdrachtBeoordelingDto> Beoordelingen { get; set; } = new();
    public List<BeoordelingCritereaDto> BeoordelingCritereas { get; set; } = new();
}
