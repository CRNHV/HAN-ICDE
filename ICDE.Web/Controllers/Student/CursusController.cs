using System;
using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Student;

[Route("student/cursus")]
[Authorize(Roles = UserRole.Student)]
public class CursusController : ControllerBase
{
    private readonly ICursusService _cursusService;

    public CursusController(ICursusService cursusService)
    {
        _cursusService = cursusService;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var cursussen = await _cursusService.AlleUnieke();
        return View("/Views/Student/Cursus/Index.cshtml", cursussen);
    }

    /// <summary>
    /// UC20
    /// </summary>
    /// <param name="cursusGroupId"></param>
    /// <returns></returns>
    [HttpGet("get/{cursusGroupId}")]
    public async Task<IActionResult> BekijkCursus([FromRoute] Guid cursusGroupId)
    {
        var cursus = await _cursusService.HaalAlleDataOp(cursusGroupId);
        if (cursus == null)
        {
            return NotFound();
        }
        return View("/Views/Student/Cursus/BekijkCursus.cshtml", cursus);
    }
}
