using ICDE.Lib.Dto.BeoordelingCriterea;

namespace ICDE.Lib.Dto.Opdracht;

public class StudentOpdrachtDto
{
    public int OpdrachtId { get; set; }
    public OpdrachtDto Opdracht { get; set; }
    public List<BeoordelingCritereaDto> BeoordelingCritereas { get; set; } = new();
}
