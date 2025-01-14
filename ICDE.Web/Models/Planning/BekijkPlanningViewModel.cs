﻿using System.Collections.Generic;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Dto.Planning;

namespace ICDE.Web.Models.Planning;

public class BekijkPlanningViewModel
{
    public PlanningDto Planning { get; set; }
    public List<LesMetLeeruitkomstenDto> LessenInPlanning { get; set; } = new();
    public List<CursusDto> Cursussen { get; set; } = new();
    public List<LesDto> Lessen { get; set; } = new();
    public List<OpdrachtDto> Opdrachten { get; set; } = new();
}
