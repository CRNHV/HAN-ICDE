namespace ICDE.Lib.Validator.Interfaces;

public interface IValidationManager
{
    void RegisterValidator(IValidator validator);
    List<ValidationResult> ValidateAll();
}