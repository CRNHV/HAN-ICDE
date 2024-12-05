using ICDE.Data.Entities;
using ICDE.Lib.Validator;

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
                Success = true,
            };
        }

        // Collect all Leeruitkomsten from PlanningItems (Les or Opdracht)
        var allPlanningItemsLeeruitkomsten = _cursus.Planning.PlanningItems
            .SelectMany(pi => pi.Opdracht != null
                ? pi.Opdracht.BeoordelingCritereas.SelectMany(bc => bc.Leeruitkomsten)
                : pi.Les?.Leeruitkomsten ?? Enumerable.Empty<Leeruitkomst>())
            .Distinct()
            .ToList();

        // Check if all Leeruitkomsten in PlanningItems are in the Cursus
        if (!allPlanningItemsLeeruitkomsten.All(l => _cursus.Leeruitkomsten.Contains(l)))
        {
            return new ValidationResult()
            {
                Success = false,
                Message = $"Not all required leeruitkomsten are covered by the planning in the cursus {_cursus.Naam}"
            };
        }

        // Ensure there are no extra Leeruitkomsten in PlanningItems that aren't in the Cursus
        if (allPlanningItemsLeeruitkomsten.Any(l => !_cursus.Leeruitkomsten.Contains(l)))
        {
            return new ValidationResult()
            {
                Success = false,
                Message = $"There are extra leeruitkomsten planned in the planning of {_cursus.Naam} which aren't in the cursus itself."
            };
        }

        // Check if all Leeruitkomsten in the Cursus are covered by the PlanningItems
        var cursusLeeruitkomsten = _cursus.Leeruitkomsten.ToList();
        if (!cursusLeeruitkomsten.All(l => allPlanningItemsLeeruitkomsten.Contains(l)))
        {
            return new ValidationResult()
            {
                Success = false,
                Message = $"There are leeruitkomsten in cursus {_cursus.Naam} that are not covered by the planning items."
            };
        }

        return new ValidationResult()
        {
            Success = true,
        };
    }
}
