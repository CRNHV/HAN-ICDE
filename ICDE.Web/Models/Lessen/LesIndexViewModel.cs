using System.Collections.Generic;
using ICDE.Lib.Dto.Lessen;

namespace ICDE.Web.Models.Lessen;

public class LesIndexViewModel
{
    public List<LesDto> Lessen { get; set; } = new List<LesDto>();
}
