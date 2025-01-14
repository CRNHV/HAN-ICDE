using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Base;

namespace ICDE.Data.Entities;

public class Les : OnderwijsOnderdeel, IVersionable, ICloneable
{
    public List<Leeruitkomst> Leeruitkomsten { get; set; } = new();
    public string? LesInhoud { get; set; }

    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }

    public object Clone()
    {
        return new Les()
        {
            Naam = Naam,
            Beschrijving = Beschrijving,
            Leeruitkomsten = Leeruitkomsten,
            LesInhoud = LesInhoud,
        };
    }
}
