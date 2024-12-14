using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Base;

namespace ICDE.Data.Entities;

public class BeoordelingCriterea : OnderwijsOnderdeel, IVersionable, ICloneable
{
    public List<Leeruitkomst> Leeruitkomsten { get; set; } = new();

    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }

    public object Clone()
    {
        return new BeoordelingCriterea()
        {
            Beschrijving = Beschrijving,
            Naam = Naam,
            Leeruitkomsten = Leeruitkomsten
        };
    }
}
