using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Cursus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/cursus")]
[Authorize(Roles = UserRole.Auteur)]
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
        return View("/Views/Auteur/Cursus/Index.cshtml", cursussen);
    }

    [HttpGet("get/{cursusGroupId}")]
    public async Task<IActionResult> BekijkCursus([FromRoute] Guid cursusGroupId)
    {
        var cursus = await _cursusService.HaalAlleDataOp(cursusGroupId);
        if (cursus is null)
        {
            return NotFound();
        }
        List<CursusDto> eerdereVersies = await _cursusService.EerdereVersies(cursusGroupId, cursus.Id);
        return View("/Views/Auteur/Cursus/BekijkCursus.cshtml", new BekijkCursusViewModel()
        {
            Cursus = cursus,
            EerderVersies = eerdereVersies,
        });
    }
}
