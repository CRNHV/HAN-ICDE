using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Base;

namespace ICDE.Data.Entities;

public class Cursus : OnderwijsOnderdeel, IVersionable, ICloneable
{
    public Planning? Planning { get; set; }
    public List<Leeruitkomst> Leeruitkomsten { get; set; } = new();

    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }

    public object Clone()
    {
        return new Cursus()
        {
            Planning = (Planning?)Planning?.Clone(),
            Leeruitkomsten = Leeruitkomsten,
            Beschrijving = Beschrijving,
            Naam = Naam,
        };
    }
}
