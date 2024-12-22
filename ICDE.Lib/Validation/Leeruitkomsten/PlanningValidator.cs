using ICDE.Data.Entities;

namespace ICDE.Lib.Validation.Leeruitkomsten;
public class PlanningValidator : IValidator
{
    private readonly Planning _planning;

    public PlanningValidator(Planning planning)
    {
        _planning = planning;
    }

    public ValidationResult Validate()
    {
        var sortedPlanningItems = _planning.PlanningItems.OrderBy(pi => pi.Index).ToList();
        var coveredLeeruitkomsten = new HashSet<Leeruitkomst>();
        var result = new ValidationResult();
        bool isSucces = true;

        foreach (var planningItem in sortedPlanningItems)
        {
            if (planningItem.Les != null)
            {
                foreach (var leeruitkomst in planningItem.Les.Leeruitkomsten)
                {
                    coveredLeeruitkomsten.Add(leeruitkomst);
                }
            }

            if (planningItem.Opdracht != null)
            {
                foreach (var beoordelingsCriterea in planningItem.Opdracht.BeoordelingCritereas)
                {
                    foreach (var leeruitkomst in beoordelingsCriterea.Leeruitkomsten)
                    {
                        if (!coveredLeeruitkomsten.Contains(leeruitkomst))
                        {
                            isSucces = false;
                            result.Messages.Add($"Planning: {_planning.Name} toetst {leeruitkomst.Naam} voordat de leeruitkomst behandeld wordt in een les.");
                        }
                    }
                }
            }
        }

        result.Success = isSucces;
        if (!isSucces)
        {
            return result;
        }

        result.Messages.Add($"Planning: {_planning.Name} is succesvol gevalideerd.");
        return result;
    }
}