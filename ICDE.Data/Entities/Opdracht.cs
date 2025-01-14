using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Base;

namespace ICDE.Data.Entities;

public class Opdracht : OnderwijsOnderdeel, IVersionable, ICloneable
{
    public OpdrachtType Type { get; set; }
    public DateTime DeadlineUtc { get; set; }

    public List<BeoordelingCriterea> BeoordelingCritereas { get; set; } = new List<BeoordelingCriterea>();
    public List<IngeleverdeOpdracht> IngeleverdeOpdrachten { get; set; } = new List<IngeleverdeOpdracht>();

    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }

    public object Clone()
    {
        return new Opdracht()
        {
            Naam = Naam,
            Beschrijving = Beschrijving,
            Type = Type,
            BeoordelingCritereas = BeoordelingCritereas,
        };
    }
}
