using ICDE.Data.Entities;
using ICDE.Lib.Validator;

public class VakValidator : IValidator
{
    private readonly Vak _vak;

    public VakValidator(Vak vak)
    {
        _vak = vak;
    }

    public ValidationResult Validate()
    {
        // Collect all Leeruitkomsten from all Cursussen
        var allCursusLeeruitkomsten = _vak.Cursussen
            .SelectMany(c => c.Leeruitkomsten)
            .Distinct()
            .ToList();

        // Ensure there are no extra Leeruitkomsten in Cursussen that aren't in the Vak
        if (allCursusLeeruitkomsten.Any(l => !_vak.Leeruitkomsten.Contains(l)))
        {
            return new ValidationResult()
            {
                Success = false,
                Message = $"There are extra leeruitkomsten in the cursussen for vak {_vak.Naam} which aren't in the vak."
            };
        }

        // Check if all Leeruitkomsten in the Vak are covered by the Cursussen
        if (!_vak.Leeruitkomsten.All(l => allCursusLeeruitkomsten.Contains(l)))
        {
            return new ValidationResult()
            {
                Success = false,
                Message = $"There are leeruitkomsten in vak {_vak.Naam} that are not covered by any cursus."
            };
        }

        return new ValidationResult()
        {
            Success = true,
        };
    }
}
