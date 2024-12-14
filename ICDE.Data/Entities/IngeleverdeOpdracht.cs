using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICDE.Data.Entities;
public class IngeleverdeOpdracht
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey("Opdracht")]
    public int OpdrachtId { get; set; }
    public int StudentNummer { get; set; }


    public string Naam { get; set; }
    public string Locatie { get; set; }

    public Opdracht Opdracht { get; set; }
    public List<OpdrachtBeoordeling> Beoordelingen { get; set; } = new();
}
