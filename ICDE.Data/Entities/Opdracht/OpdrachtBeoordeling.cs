using System.ComponentModel.DataAnnotations;

namespace ICDE.Data.Entities.Opdracht;
public class OpdrachtBeoordeling
{
    [Key]
    public int Id { get; set; }
    public double Cijfer { get; set; }
    public string Feedback { get; set; }

    public IngeleverdeOpdracht IngeleverdeOpdracht { get; set; }
}
