using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Dto.Opdracht;
public class OpdrachtUpdateDto
{
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public bool IsToets { get; set; }
    public Guid GroupId { get; set; }
}
