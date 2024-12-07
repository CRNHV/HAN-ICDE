using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult HandleError(int statusCode)
    {
        if (statusCode < 500)
        {
            return View($"{statusCode}");
        }

        return View("Error");
    }
}
