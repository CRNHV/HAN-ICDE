﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Validator;
public class ValidationResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}