using System.Collections.Generic;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Dto.Vak;

namespace ICDE.Web.Models.Vakken;

public class BekijkVakViewModel
{
    public VakDto Vak { get; set; }
    public List<VakDto> EerdereVersies { get; set; } = new();
    public List<LeeruitkomstDto> Leeruitkomsten { get; set; } = new();
    public List<CursusDto> Cursussen { get; set; } = new();

}
