using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICDE.Data.Entities.Base;

public abstract class OnderwijsOnderdeel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    //public int VersieNummer { get; set; }
    //public Guid GroupId { get; set; }

    //[NotMapped]
    //public bool RelationshipChanged { get; set; }
}
