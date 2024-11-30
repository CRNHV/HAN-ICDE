using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Dto.Cursus;
public class CursusMetEerdereVersiesDto
{

    CursusMetPlanningDto Cursus { get; set; }
    public List<CursusDto> EerdereVersies { get; set; } = new();
}
