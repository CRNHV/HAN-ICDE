using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICDE.Data.Entities;
public class Planning : ICloneable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }

    public List<PlanningItem> PlanningItems { get; set; }

    public object Clone()
    {
        return new Planning()
        {
            Name = this.Name,
            PlanningItems = this.PlanningItems
                .Select(x => (PlanningItem)x.Clone())
                .ToList(),
        };
    }
}
