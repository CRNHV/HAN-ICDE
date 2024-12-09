using System.Collections.Generic;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Dto.Vak;

namespace ICDE.Web.Models.Rapportage;

public class RapportageIndexViewModel
{
    public List<OpleidingDto> Opleidingen { get; set; } = new();
    public List<VakDto> Vakken { get; set; } = new();
    public List<CursusDto> Cursussen { get; set; } = new();
}
