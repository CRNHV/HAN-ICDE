using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICDE.Data.Entities;

public class Vak : IOnderwijsOnderdeel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }
    public List<Leeruitkomst> Leeruitkomsten { get; set; } = new();
    public List<Cursus> Cursussen { get; set; } = new();

    [NotMapped]
    public bool RelationshipChanged { get; set; }
}
