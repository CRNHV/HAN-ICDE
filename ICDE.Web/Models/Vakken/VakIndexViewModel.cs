using System.Collections.Generic;
using ICDE.Lib.Dto.Vak;

namespace ICDE.Web.Models.Vakken;

public class VakIndexViewModel
{
    public List<VakDto> Vakken { get; set; } = new List<VakDto>();
}
