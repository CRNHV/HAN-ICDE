using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Base;

namespace ICDE.Data.Entities;

public class Opleiding : OnderwijsOnderdeel, IVersionable, ICloneable
{
    public List<Vak> Vakken { get; set; } = new();
    public Guid GroupId { get; set; }
    public int VersieNummer { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }

    public object Clone()
    {
        var vakken = Vakken.ConvertAll(x => (Vak)x.Clone()).ToList();
        return new Opleiding()
        {
            Vakken = vakken,
            Beschrijving = Beschrijving,
            Naam = Naam,
        };
    }
}
