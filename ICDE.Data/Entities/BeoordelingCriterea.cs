using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICDE.Data.Entities;

public class BeoordelingCriterea : IVersionable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }
    public List<Leeruitkomst> Leeruitkomsten { get; set; }
    public int OpdrachtId { get; internal set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }
}
