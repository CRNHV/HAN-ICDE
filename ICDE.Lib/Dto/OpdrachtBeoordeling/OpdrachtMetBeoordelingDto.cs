﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Dto.OpdrachtBeoordeling;
public class OpdrachtMetBeoordelingDto
{
    public string OpdrachtNaam { get; set; }
    public double Cijfer { get; set; }
    public string Feedback { get; set; }
}
