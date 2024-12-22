namespace ICDE.Lib.Validation.Leeruitkomsten;

public interface IValidationManager
{
    void RegisterValidator(IValidator validator);
    List<ValidationResult> ValidateAll();
}