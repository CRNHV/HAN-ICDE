using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Base;

namespace ICDE.Data.Entities;

public class Opleiding : OnderwijsOnderdeel, IVersionable
{
    public List<Vak> Vakken { get; set; } = new();
    public Guid GroupId { get; set; }
    public int VersieNummer { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }
    
}
