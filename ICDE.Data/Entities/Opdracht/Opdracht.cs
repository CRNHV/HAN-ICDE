using System.ComponentModel.DataAnnotations;
using ICDE.Data.Entities.OnderwijsOnderdeel;

namespace ICDE.Data.Entities.Opdracht;

public class Opdracht
{
    [Key]
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public OpdrachtType Type { get; set; }

    public ICollection<BeoordelingCriterea> BeoordelingCritereas { get; set; }
    public ICollection<IngeleverdeOpdracht> IngeleverdeOpdrachten { get; set; }
}
