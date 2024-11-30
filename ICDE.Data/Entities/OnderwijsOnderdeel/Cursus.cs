using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICDE.Data.Entities.OnderwijsOnderdeel;

public class Cursus : IOnderwijsOnderdeel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }
    public Planning? Planning { get; set; }
    public List<Leeruitkomst> Leeruitkomsten { get; set; } = new();

    [NotMapped]
    public bool RelationshipChanged { get; set; }
}
