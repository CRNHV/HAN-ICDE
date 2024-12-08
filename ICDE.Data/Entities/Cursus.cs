using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Base;

namespace ICDE.Data.Entities;

public class Cursus : OnderwijsOnderdeel, IVersionable
{
    public Planning? Planning { get; set; }
    public List<Leeruitkomst> Leeruitkomsten { get; set; } = new();

    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }
}
