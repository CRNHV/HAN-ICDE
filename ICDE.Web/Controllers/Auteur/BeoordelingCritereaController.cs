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
        List<BeoordelingCritereaDto> beoordelingCritereas = await _beoordelingCritereaService.Unieke();
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

        var leeruitkomsten = await _leeruitkomstService.Allemaal();
        return View("/Views/Auteur/BeoordelingCritereas/BekijkCriterea.cshtml", new BekijkBeoorderlingCritereaViewModel()
        {
            BeoordelingCriterea = beoorderlingCriterea.BeoordelingCriterea,
            Leeruitkomsten = leeruitkomsten,
            EerdereVersies = beoorderlingCriterea.EerdereVersies,
        });
    }

    [HttpGet("maak")]
    public async Task<IActionResult> Maak([FromRoute] Guid opleidingGuid)
    {
        return Ok();
    }

    [HttpGet("koppelluk")]
    public async Task<IActionResult> KoppelLuk([FromRoute] Guid opleidingGuid)
    {
        return Ok();
    }
}
