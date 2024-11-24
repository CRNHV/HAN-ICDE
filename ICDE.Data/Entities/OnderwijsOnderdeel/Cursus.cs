using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICDE.Data.Entities.OnderwijsOnderdeel;
public class Cursus : IOnderwijsOnderdeel
{
    [Key]
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public int VersieNummer { get; set; }  
    public Planning Planning { get; set; }
}
