using ICDE.Data.Entities.Identity;
using System.Security.Claims;
using ICDE.Lib.Domain.User;
using System.Security.Principal;
using System.Linq;

namespace ICDE.Web.ViewHelper;

internal static class ClaimsParser
{
    public static string? GetUserRole(IIdentity identity)
    {
        var claimsIdentity = (ClaimsIdentity)identity;
        var role = claimsIdentity.Claims.Where(x => x.Type == "Role").FirstOrDefault();
        if (role is null)
            return null;

        return role.Value;
    }
}
