using System.Collections.Generic;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Dto.Vak;

namespace ICDE.Web.Models.Opleiding;

public class BekijkOpleidingViewModel
{
    public OpleidingMetVakkenDto Opleiding { get; set; }
    public List<VakDto> BeschikbareVakken { get; set; } = new();
    public List<OpleidingDto> EerdereVersies { get; set; }
}
