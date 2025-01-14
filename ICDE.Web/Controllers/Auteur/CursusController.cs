using System;
using System.Threading.Tasks;
using ICDE.Data.Entities;
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
    private readonly ILeeruitkomstService _leeruitkomstService;

    public CursusController(ICursusService cursusService, ILeeruitkomstService leeruitkomstService)
    {
        _cursusService = cursusService;
        _leeruitkomstService = leeruitkomstService;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var cursussen = await _cursusService.AlleUnieke();
        return View("/Views/Auteur/Cursus/Index.cshtml", cursussen);
    }

    [HttpPost("Maak")]
    public async Task<IActionResult> MaakCursus([FromForm] MaakCursusDto request)
    {
        var result = await _cursusService.Maak(request);
        if (result is null)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/cursus/get/{result.GroupId}");
    }

    [HttpGet("get/{cursusGroupId}")]
    public async Task<IActionResult> BekijkCursus([FromRoute] Guid cursusGroupId)
    {
        var cursus = await _cursusService.HaalAlleDataOp(cursusGroupId);
        if (cursus is null)
        {
            return NotFound();
        }

        var eerdereVersies = await _cursusService.EerdereVersies(cursusGroupId, cursus.VersieNummer);
        var luks = await _leeruitkomstService.AlleUnieke();

        return View("/Views/Auteur/Cursus/BekijkCursus.cshtml", new BekijkCursusViewModel()
        {
            Cursus = cursus,
            EerderVersies = eerdereVersies,
            Leeruitkomsten = luks,
        });
    }

    [HttpGet("{cursusGroupId}/koppelluk/{lukGroupId}")]
    public async Task<IActionResult> KoppelLuk([FromRoute] Guid cursusGroupId, [FromRoute] Guid lukGroupId)
    {
        bool result = await _cursusService.KoppelLuk(cursusGroupId, lukGroupId);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/cursus/get/{cursusGroupId}");
    }

    [HttpGet("{cursusGroupId}/verwijderluk/{lukGroupId}")]
    public async Task<IActionResult> OntkoppelLuk([FromRoute] Guid cursusGroupId, [FromRoute] Guid lukGroupId)
    {
        bool result = await _cursusService.OntkoppelLuk(cursusGroupId, lukGroupId);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/cursus/get/{cursusGroupId}");
    }

    [HttpGet("{groupId}/bekijkversie/{versieId}")]
    public async Task<IActionResult> BekijkVersie([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        var cursus = await _cursusService.BekijkVersie(groupId, versieId);
        if (cursus is null)
        {
            return NotFound();
        }
        return View("/Views/Auteur/Cursus/BekijkVersie.cshtml", cursus);
    }

    [HttpGet("{groupId}/kopie/{versieId}")]
    public async Task<IActionResult> MaakKopieVanVersie([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        Guid cursus = await _cursusService.MaakKopie(groupId, versieId);
        if (cursus == Guid.Empty)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/cursus/get/{cursus}");
    }

    [HttpGet("delete/{groupId}/{versieId}")]
    public async Task<IActionResult> VerwijderVersie([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        var result = await _cursusService.VerwijderVersie(groupId, versieId);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/cursus/index");
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateCursus([FromForm] UpdateCursusDto request)
    {
        var cursus = await _cursusService.Update(request);
        if (cursus is false)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/cursus/get/{request.GroupId}");
    }
}
