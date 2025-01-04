using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Opdrachten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ICDE.Web.Controllers.Auteur;


[Route("auteur/opdracht")]
[Authorize(Roles = UserRole.Auteur)]
public class OpdrachtController : ControllerBase
{
    private readonly IOpdrachtService _opdrachtService;
    private readonly IBeoordelingCritereaService _beoordeilngCritereaService;

    public OpdrachtController(IOpdrachtService opdrachtService, IBeoordelingCritereaService beoordeilngCritereaService)
    {
        _opdrachtService = opdrachtService;
        _beoordeilngCritereaService = beoordeilngCritereaService;
    }

    [HttpGet("bekijkalle")]
    public async Task<IActionResult> BekijkAlle()
    {
        List<OpdrachtDto> opdrachten = await _opdrachtService.AlleUnieke();
        return View("/Views/Auteur/Opdracht/BekijkAlle.cshtml", opdrachten);
    }

    /// <summary>
    /// UC8
    /// </summary>
    /// <returns></returns>
    [HttpGet("maak")]
    [HttpPost("maak")]
    public async Task<IActionResult> MaakOpdracht([FromForm] MaakOpdrachtDto request)
    {
        if (IsRequestMethod("POST"))
        {
            await _opdrachtService.Maak(request);
        }

        return View("/Views/Auteur/Opdracht/MaakOpdracht.cshtml");
    }

    [HttpGet("{opdrachtGroupId}/voegcritereatoe/{critereaGroupId}")]
    public async Task<IActionResult> AddCritereaToAssignment([FromRoute] Guid opdrachtGroupId, [FromRoute] Guid critereaGroupId)
    {
        bool result = await _opdrachtService.VoegCritereaToe(opdrachtGroupId, critereaGroupId);
        return Redirect($"/auteur/opdracht/bekijk/{opdrachtGroupId}");
    }

    [HttpGet("bekijk/{opdrachtGroupId}")]
    public async Task<IActionResult> BekijkOpdracht([FromRoute] Guid opdrachtGroupId)
    {
        var opdrachtData = await _opdrachtService.HaalAlleDataOp(opdrachtGroupId);
        if (opdrachtData is null)
        {
            return NotFound();
        }

        var beoordelingCritereas = await _beoordeilngCritereaService.AlleUnieke();

        return View("/Views/Auteur/Opdracht/BekijkOpdracht.cshtml", new AuteurBekijkOpdrachtViewModel()
        {
            Opdracht = opdrachtData,
            BeoordelingCritereas = beoordelingCritereas
        });
    }

    /// <summary>
    /// UC8
    /// </summary>
    /// <returns></returns>
    [HttpGet("verwijder/{groupId}/{versie}")]
    public async Task<IActionResult> VerwijderOpdracht([FromRoute] Guid groupId, [FromRoute] int versie)
    {
        bool result = await _opdrachtService.VerwijderVersie(groupId, versie);
        if (!result)
        {
            return BadRequest();
        }

        return RedirectToAction("BekijkAlle");
    }

    /// <summary>
    /// UC8
    /// </summary>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<IActionResult> UpdateOpdracht([FromForm] UpdateOpdrachtDto request)
    {
        await _opdrachtService.Update(request);
        return Redirect($"/auteur/opdracht/bekijk/{request.GroupId}");
    }
}
