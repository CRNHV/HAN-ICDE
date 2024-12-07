﻿using System.Collections.Generic;
using ICDE.Lib.Dto.Opdracht;

namespace ICDE.Web.Models.Opdrachten;

public class BekijkOpdrachtViewModel
{
    public OpdrachtDto Opdracht { get; set; }
    public List<IngeleverdeOpdrachtDto> Inzendingen { get; set; } = new();
    public List<OpdrachtBeoordelingDto> Beoordelingen { get; set; } = new();
}
