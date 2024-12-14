using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Base;

namespace ICDE.Data.Entities;

public class Leeruitkomst : OnderwijsOnderdeel, IVersionable, ICloneable
{
    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }

    public object Clone()
    {
        return new Leeruitkomst()
        {
            Naam = Naam,
            Beschrijving = Beschrijving,
        };
    }
}