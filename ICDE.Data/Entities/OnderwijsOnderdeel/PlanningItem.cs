using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Opdrachten;

namespace ICDE.Data.Entities.OnderwijsOnderdeel;
public class PlanningItem
{
    [Key]
    public int Id { get; set; }

    public int PlanningId { get; set; }
    public Planning Planning { get; set; } 

    public int? OpdrachtId { get; set; }
    public Opdracht Opdracht { get; set; }

    public int? LesId { get; set; }
    public Les Les { get; set; }

    public int Index { get; set; }
}

