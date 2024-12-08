using System.Collections.Generic;
using ICDE.Lib.Dto.Cursus;

namespace ICDE.Web.Models.Cursus;

public class BekijkCursusViewModel
{
    public CursusMetPlanningDto Cursus { get; set; }
    public List<CursusDto> EerderVersies { get; set; } = new();
}
