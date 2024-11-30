using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Dto.Opleidingen;
public class OpleidingMetEerdereVersiesDto
{
    public OpleidingDto OpleidingDto { get; set; }
    public List<OpleidingDto> EerdereVersies { get; set; }

}
