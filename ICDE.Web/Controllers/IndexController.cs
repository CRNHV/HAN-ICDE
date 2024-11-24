using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers;

[Route("/")]
public class IndexController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
