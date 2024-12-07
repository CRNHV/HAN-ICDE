using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICDE.Lib.Validator.Interfaces;

namespace ICDE.Lib.Validator;
public class ValidationManager : IValidationManager
{
    private readonly List<IValidator> _validators;

    public ValidationManager()
    {
        _validators = new List<IValidator>();
    }

    public void RegisterValidator(IValidator validator)
    {
        _validators.Add(validator);
    }

    public List<ValidationResult> ValidateAll()
    {
        return _validators
            .ConvertAll(v => v.Validate())
;
    }
}