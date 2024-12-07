using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Base;

namespace ICDE.Data.Entities;

public class BeoordelingCriterea : OnderwijsOnderdeel, IVersionable
{
    public List<Leeruitkomst> Leeruitkomsten { get; set; }
    public int OpdrachtId { get; internal set; }

    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }
}
