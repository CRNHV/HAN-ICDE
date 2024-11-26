using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Dto.Lessen;
public class LesDto
{
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }
}
