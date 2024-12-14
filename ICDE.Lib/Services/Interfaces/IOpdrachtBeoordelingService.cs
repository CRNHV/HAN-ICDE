﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICDE.Lib.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Services.Base;

namespace ICDE.Lib.Services.Interfaces;
public interface IOpdrachtBeoordelingService
{
    Task<List<OpdrachtMetBeoordelingDto>> HaalBeoordelingenOpVoorUser(int? userId);
}
