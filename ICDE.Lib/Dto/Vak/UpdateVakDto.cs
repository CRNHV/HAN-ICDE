using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Dto.Vak;
public class UpdateVakDto
{
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public Guid GroupId { get; set; }
}
