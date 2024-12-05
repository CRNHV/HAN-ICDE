using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Validator;
public interface IValidator
{
    ValidationResult Validate();
}