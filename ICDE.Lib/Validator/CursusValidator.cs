using ICDE.Data.Entities;
using ICDE.Lib.Validator;
using ICDE.Lib.Validator.Interfaces;

public class CursusValidator : IValidator
{
    private readonly Cursus _cursus;

    public CursusValidator(Cursus cursus)
    {
        _cursus = cursus;
    }

    public ValidationResult Validate()
    {
        if (_cursus.Planning is null)
        {
            return new ValidationResult()
            {
                Message = $"Cursus: {_cursus.Naam} does not have a planning.",
                Success = false,
            };
        }

        var allPlanningItemsLeeruitkomsten = _cursus.Planning.PlanningItems
            .SelectMany(pi => pi.Opdracht != null
                ? pi.Opdracht.BeoordelingCritereas.SelectMany(bc => bc.Leeruitkomsten)
                : pi.Les?.Leeruitkomsten ?? Enumerable.Empty<Leeruitkomst>())
            .Distinct()
            .ToList();

        if (!allPlanningItemsLeeruitkomsten.All(l => _cursus.Leeruitkomsten.Contains(l)))
        {
            return new ValidationResult()
            {
                Success = false,
                Message = $"Cursus: {_cursus.Naam}'s planning does not cover all leeruitkomsten expected.",
            };
        }

        if (allPlanningItemsLeeruitkomsten.Any(l => !_cursus.Leeruitkomsten.Contains(l)))
        {
            return new ValidationResult()
            {
                Success = false,
                Message = $"Cursus: {_cursus.Naam}'s planning has extra leeruitkomsten which aren't expected by the cursus."
            };
        }

        var cursusLeeruitkomsten = _cursus.Leeruitkomsten.ToList();
        if (!cursusLeeruitkomsten.All(l => allPlanningItemsLeeruitkomsten.Contains(l)))
        {
            return new ValidationResult()
            {
                Success = false,
                Message = $"Cursus: {_cursus.Naam}'s planning does not cover all leeruitkomsten expected.",
            };
        }

        return new ValidationResult()
        {
            Message = $"Cursus: {_cursus.Naam} has passed validation.",
            Success = true,
        };
    }
}
