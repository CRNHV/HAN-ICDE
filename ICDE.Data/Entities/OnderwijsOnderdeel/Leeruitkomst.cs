using System.ComponentModel.DataAnnotations;

namespace ICDE.Data.Entities.OnderwijsOnderdeel;
public class Leeruitkomst : IOnderwijsOnderdeel
{
    [Key]
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public int VersieNummer { get; set; }
}