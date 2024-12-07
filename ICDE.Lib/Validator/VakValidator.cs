using ICDE.Data.Entities;
using ICDE.Lib.Validator;
using ICDE.Lib.Validator.Interfaces;

public class VakValidator : IValidator
{
    private readonly Vak _vak;

    public VakValidator(Vak vak)
    {
        _vak = vak;
    }

    public ValidationResult Validate()
    {
        var allCursusLeeruitkomsten = _vak.Cursussen
            .SelectMany(c => c.Leeruitkomsten)
            .Distinct()
            .ToList();

        if (allCursusLeeruitkomsten.Any(l => !_vak.Leeruitkomsten.Contains(l)))
        {
            return new ValidationResult()
            {
                Success = false,
                Message = $"Vak: {_vak.Naam}'s cursussen has leeruitkomsten which aren't expected in the vak."
            };
        }

        if (!_vak.Leeruitkomsten.All(l => allCursusLeeruitkomsten.Contains(l)))
        {
            return new ValidationResult()
            {
                Success = false,
                Message = $"Vak: {_vak.Naam}'s leeruitkomsten are not covered by all cursussen.",
            };
        }

        return new ValidationResult()
        {
            Message = $"Vak: {_vak.Naam} has passed the validation.",
            Success = true,
        };
    }
}
