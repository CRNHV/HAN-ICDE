using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/cursussen")]
public class CursusController : ControllerBase
{
    /// <summary>
    /// UC17
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public async Task<IActionResult> CheckLeeruitkomstOverzichtVanCursus()
    {
        return View();
    }
}
