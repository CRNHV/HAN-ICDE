using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Student;

[Route("student/cursus")]
public class CursusController : ControllerBase
{
    /// <summary>
    /// UC20
    /// </summary>
    /// <param name="cursusGroupId"></param>
    /// <returns></returns>
    [HttpGet("{cursusGroupId}")]
    public async Task<IActionResult> BekijkCursus([FromRoute] Guid cursusGroupId)
    {
        // Haal cursus incl planning op
        return View();
    }
}
