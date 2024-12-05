using ICDE.Data.Entities;

namespace ICDE.Lib.Validator;
public class OpleidingValidator
{
    public List<ValidationResult> ValidateOpleiding(Opleiding opleiding)
    {
        var validationManager = new ValidationManager();

        foreach (var vak in opleiding.Vakken)
        {
            validationManager.RegisterValidator(new VakValidator(vak));
            foreach (var cursus in opleiding.Vakken.SelectMany(x => x.Cursussen).ToList())
            {
                validationManager.RegisterValidator(new CursusValidator(cursus));
                if (cursus.Planning != null)
                    validationManager.RegisterValidator(new PlanningValidator(cursus.Planning));
            }
        }

        return validationManager.ValidateAll();
    }
}