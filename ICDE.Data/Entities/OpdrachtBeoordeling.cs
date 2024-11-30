using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICDE.Data.Entities;
public class OpdrachtBeoordeling
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public double Cijfer { get; set; }
    public string Feedback { get; set; }

    public IngeleverdeOpdracht IngeleverdeOpdracht { get; set; }
}
