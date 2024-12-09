using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Core;
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
        List<OpdrachtDto> opdrachten = await _opdrachtService.GetAll();
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
            await _opdrachtService.MaakOpdracht(request);
        }

        return View("/Views/Auteur/Opdracht/MaakOpdracht.cshtml");
    }

    [HttpGet("{opdrachtGroupId}/voegcritereatoe/{critereaGroupId}")]
    public async Task<IActionResult> AddCritereaToAssignment([FromRoute] Guid opdrachtGroupId, [FromRoute] Guid critereaGroupId)
    {
        bool result = await _opdrachtService.AddCritereaToAssignment(opdrachtGroupId, critereaGroupId);
        return Redirect($"/auteur/opdracht/bekijk/{opdrachtGroupId}");
    }

    [HttpGet("bekijk/{opdrachtGroupId}")]
    public async Task<IActionResult> BekijkOpdracht([FromRoute] Guid opdrachtGroupId)
    {
        var opdrachtData = await _opdrachtService.GetFullDataByGroupId(opdrachtGroupId);
        if (opdrachtData is null)
        {
            return NotFound();
        }

        var beoordelingCritereas = await _beoordeilngCritereaService.GetAllUnique();

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
    [HttpGet("{opdrachtGroupId}/verwijder")]
    public async Task<IActionResult> VerwijderOpdracht([FromRoute] Guid opdrachtGroupId)
    {
        await _opdrachtService.VerwijderOpdracht(opdrachtGroupId);
        return Redirect("/auteur/opdracht/bekijkalle");
    }

    /// <summary>
    /// UC8
    /// </summary>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<IActionResult> UpdateOpdracht([FromForm] OpdrachtUpdateDto request)
    {
        await _opdrachtService.UpdateOpdracht(request);
        return Redirect($"/auteur/opdracht/bekijk/{request.GroupId}");
    }
}
