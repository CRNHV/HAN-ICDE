using System;
using System.Collections.Generic;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Dto.Lessen;

namespace ICDE.Web.Models.Lessen;

public class BekijkLesViewModel
{
    public LesDto Les { get; set; }
    public List<LeeruitkomstDto> LesLeeruitkomsten { get; set; } = new();
    public List<LeeruitkomstDto> BeschrikbareLeeruitkomsten { get; set; } = new();
    public List<LesDto> LesList { get; set; } = new List<LesDto>();
}
