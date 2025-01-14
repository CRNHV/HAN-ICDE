using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.BeoordelingCriterea;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/criterea")]
[Authorize(Roles = UserRole.Auteur)]
public class BeoordelingCritereaController : ControllerBase
{
    private readonly IBeoordelingCritereaService _beoordelingCritereaService;
    private readonly ILeeruitkomstService _leeruitkomstService;

    public BeoordelingCritereaController(IBeoordelingCritereaService beoordelingCritereaService, ILeeruitkomstService leeruitkomstService)
    {
        _beoordelingCritereaService = beoordelingCritereaService;
        _leeruitkomstService = leeruitkomstService;
    }

    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        List<BeoordelingCritereaDto> beoordelingCritereas = await _beoordelingCritereaService.AlleUnieke();
        return View("/Views/Auteur/BeoordelingCritereas/index.cshtml", beoordelingCritereas);
    }

    [HttpGet("get/{critereaGroupId}")]
    public async Task<IActionResult> Index([FromRoute] Guid critereaGroupId)
    {
        var beoorderlingCriterea = await _beoordelingCritereaService.HaalOpMetEerdereVersies(critereaGroupId);
        if (beoorderlingCriterea is null)
        {
            return NotFound();
        }

        var leeruitkomsten = await _leeruitkomstService.AlleUnieke();
        return View("/Views/Auteur/BeoordelingCritereas/BekijkCriterea.cshtml", new BekijkBeoorderlingCritereaViewModel()
        {
            BeoordelingCriterea = beoorderlingCriterea.BeoordelingCriterea,
            Leeruitkomsten = leeruitkomsten,
            EerdereVersies = beoorderlingCriterea.EerdereVersies,
        });
    }

    [HttpPost]
    public async Task<IActionResult> Maak([FromForm] MaakBeoordelingCritereaDto request)
    {
        var result = await _beoordelingCritereaService.Maak(request);
        if (result is null)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/criterea/get/{result.GroupId}");
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateBeoordelingCriterea([FromForm] UpdateBeoordelingCritereaDto request)
    {
        var result = await _beoordelingCritereaService.Update(request);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/criterea/get/{request.GroupId}");
    }

    [HttpGet("{beoordelingCritereaGroupId}/koppelluk/{leeruitkomstGroupId}")]
    public async Task<IActionResult> KoppelLuk([FromRoute] Guid beoordelingCritereaGroupId, [FromRoute] Guid leeruitkomstGroupId)
    {
        bool result = await _beoordelingCritereaService.KoppelLeeruitkomst(beoordelingCritereaGroupId, leeruitkomstGroupId);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/criterea/get/{beoordelingCritereaGroupId}");
    }

    [HttpGet("{beoordelingCritereaGroupId}/VerwijderLuk/{leeruitkomstGroupId}")]
    public async Task<IActionResult> VerwijderLuk([FromRoute] Guid beoordelingCritereaGroupId, [FromRoute] Guid leeruitkomstGroupId)
    {
        bool result = await _beoordelingCritereaService.VerwijderLuk(beoordelingCritereaGroupId, leeruitkomstGroupId);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/criterea/get/{beoordelingCritereaGroupId}");
    }

    [HttpGet("{groupId}/bekijkversie/{versieId}")]
    public async Task<IActionResult> BekijkVersie([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        var cursus = await _beoordelingCritereaService.BekijkVersie(groupId, versieId);
        if (cursus is null)
        {
            return NotFound();
        }
        return View("/Views/Auteur/BeoordelingCritereas/BekijkVersie.cshtml", cursus);

    }

    [HttpGet("{groupId}/kopie/{versieId}")]
    public async Task<IActionResult> MaakKopieVanVersie([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        Guid cursus = await _beoordelingCritereaService.MaakKopie(groupId, versieId);
        if (cursus == Guid.Empty)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/BeoordelingCritereas/get/{cursus}");

    }

    [HttpGet("delete/{groupId}/{versieId}")]
    public async Task<IActionResult> VerwijderVersie([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        var result = await _beoordelingCritereaService.VerwijderVersie(groupId, versieId);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/BeoordelingCritereas/index");
    }
}
