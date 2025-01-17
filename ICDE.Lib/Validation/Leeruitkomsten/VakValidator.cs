using ICDE.Data.Entities;
using ICDE.Lib.Validation.Leeruitkomsten;

namespace ICDE.Lib.Validation.Leeruitkomsten;

public class VakValidator : IValidator
{
    private readonly Vak _vak;

    public VakValidator(Vak vak)
    {
        _vak = vak;
    }

    public ValidationResult Validate()
    {
        var result = new ValidationResult();
        var allCursusLeeruitkomsten = _vak.Cursussen
            .SelectMany(c => c.Leeruitkomsten)
            .Distinct()
            .ToList();

        result.Messages.Add($"Vak: {_vak.Naam} heeft verwachte leeruitkomsten: {String.Join(",", _vak.Leeruitkomsten.Select(x => x.Naam))}");

        if (allCursusLeeruitkomsten.Any(l => !_vak.Leeruitkomsten.Contains(l)))
        {
            var extra = allCursusLeeruitkomsten.Except(_vak.Leeruitkomsten);

            result.Success = false;
            result.Messages.Add($"Vak: {_vak.Naam} heeft de volgende extra leeruitkomsten in de cursussen: {String.Join(",", extra.Select(x => x.Naam))}.");
            return result;
        }

        if (!_vak.Leeruitkomsten.All(l => allCursusLeeruitkomsten.Contains(l)))
        {
            var missing = _vak.Leeruitkomsten.Except(allCursusLeeruitkomsten);

            result.Success = false;
            result.Messages.Add($"Vak: {_vak.Naam} heeft cursussen die niet alle leeruitkomsten dekken. Missende leeruitkomsten: {String.Join(",", missing.Select(x => x.Naam))}.");
            return result;
        }

        result.Success = true;
        result.Messages.Add($"Vak: {_vak.Naam} has passed the validation.");

        return result;
    }
}
