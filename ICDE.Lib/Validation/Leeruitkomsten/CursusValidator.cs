using ICDE.Data.Entities;
using ICDE.Lib.Validation.Leeruitkomsten;

namespace ICDE.Lib.Validation.Leeruitkomsten;

public class CursusValidator : IValidator
{
    private readonly Cursus _cursus;

    public CursusValidator(Cursus cursus)
    {
        _cursus = cursus;
    }

    public ValidationResult Validate()
    {
        var result = new ValidationResult();

        if (_cursus.Planning is null)
        {
            result.Messages.Add($"Cursus: {_cursus.Naam} heeft geen planning.");
            result.Success = false;
            return result;
        }

        var allPlanningItemsLeeruitkomsten = _cursus.Planning.PlanningItems
            .SelectMany(pi => pi.Opdracht != null
                ? pi.Opdracht.BeoordelingCritereas.SelectMany(bc => bc.Leeruitkomsten)
                : pi.Les?.Leeruitkomsten ?? Enumerable.Empty<Leeruitkomst>())
            .Distinct()
            .ToList();

        result.Messages.Add($"Cursus: {_cursus.Naam} heeft verwachte leeruitkomsten: {String.Join(",", allPlanningItemsLeeruitkomsten.Select(x => x.Naam))}");

        if (!allPlanningItemsLeeruitkomsten.All(l => _cursus.Leeruitkomsten.Contains(l)))
        {
            var missing = _cursus.Leeruitkomsten.Except(allPlanningItemsLeeruitkomsten);

            result.Messages.Add($"Cursus: {_cursus.Naam} mist leeruitkomsten: {String.Join(",", missing.Select(x => x.Naam))}");
            result.Success = false;

            return result;
        }

        if (allPlanningItemsLeeruitkomsten.Any(l => !_cursus.Leeruitkomsten.Contains(l)))
        {
            var extra = allPlanningItemsLeeruitkomsten.Except(_cursus.Leeruitkomsten);

            result.Messages.Add($"Cursus: {_cursus.Naam} heeft de volgende extra leeruitkomsten in de planning: {String.Join(",", extra.Select(x => x.Naam))}");
            result.Success = false;

            return result;
        }

        result.Success = true;
        result.Messages.Add($"Cursus: {_cursus.Naam} is succesvol gevalideerd.");
        return result;
    }
}
