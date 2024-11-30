using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICDE.Lib.Dto.Vak;

namespace ICDE.Lib.Dto.Opleidingen;
public class OpleidingDto
{
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public Guid GroupId { get; set; }
    public int VersieNummer { get; set; }
    public List<VakDto> Vakken { get; set; } = new();
}
