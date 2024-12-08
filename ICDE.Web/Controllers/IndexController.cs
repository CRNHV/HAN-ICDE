using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers;

[Route("/")]
[Authorize]
public class IndexController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
