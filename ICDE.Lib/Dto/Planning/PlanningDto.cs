using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Dto.Planning;
public class PlanningDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PlanningItemDto> Items { get; set; } = new List<PlanningItemDto>();
}
