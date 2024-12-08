using System.Collections.Generic;
using ICDE.Lib.Dto.Opdracht;

namespace ICDE.Web.Models.Opdrachten;

public class OpdrachtenIndexViewModel
{
    public List<OpdrachtDto> Opdrachten { get; set; } = new();
}
