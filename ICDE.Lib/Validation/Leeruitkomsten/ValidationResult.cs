﻿namespace ICDE.Lib.Validation.Leeruitkomsten;
public class ValidationResult
{
    public bool Success { get; set; }
    public List<string> Messages { get; set; } = new();
}
