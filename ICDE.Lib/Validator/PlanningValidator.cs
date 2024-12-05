using ICDE.Data.Entities;

namespace ICDE.Lib.Validator;
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
                            return new ValidationResult()
                            {
                                Success = false,
                                Message = $"The leeruitkomst {leeruitkomst.Naam} is tested before it is covered by any of the lessons."
                            };
                        }
                    }
                }
            }
        }

        return new ValidationResult()
        {
            Success = true,
        };
    }
}