using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Dto.Lessen;
public class LesUpdateDto
{
    public Guid GroupId { get; set; }
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
}
