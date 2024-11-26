using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICDE.Data.Entities.OnderwijsOnderdeel;

public class Leeruitkomst : IOnderwijsOnderdeel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }
}