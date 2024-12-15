using System;
using System.Collections.Generic;
using ICDE.Lib.Validator;

namespace ICDE.Web.Models.Rapportage;

public class RapportageResultViewModel
{
    public bool Success { get; set; }
    public List<ValidationResult> Results { get; set; } = new();
    public Guid GroupId { get; set; }
    public string Type { get; set; }
}
