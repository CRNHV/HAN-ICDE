﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Data.Entities.OnderwijsOnderdeel;
public class Planning
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public List<PlanningItem> PlanningItems { get; set; }
}
