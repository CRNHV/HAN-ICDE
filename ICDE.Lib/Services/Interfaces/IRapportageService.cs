using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Lib.Services.Interfaces;
public interface IRapportageService
{
    Task<bool> ValidateOpleiding(Guid opleidingGroupId);
}
