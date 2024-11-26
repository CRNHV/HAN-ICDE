using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Entities.OnderwijsOnderdeel;

[PrimaryKey(nameof(Id), nameof(VersieNummer))]
public class Vak : IOnderwijsOnderdeel
{
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }
    public List<Leeruitkomst> Leeruitkomsten { get; set; }
    public List<Cursus> Cursussen { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }
}
