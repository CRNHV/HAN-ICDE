using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers;

public class ControllerBase : Controller
{
    public int? GetUserIdFromClaims()
    {
        var claim = this.HttpContext.User.Claims.Where(x => x.Type == "Id").FirstOrDefault();
        if (claim is null)
            return null;

        var userIdString = claim.Value;
        if (int.TryParse(userIdString, out var userId))
            return userId;

        return null;
    }

    public bool IsRequestMethod(string expected)
    {
        return this.HttpContext.Request.Method == expected;
    }
}
