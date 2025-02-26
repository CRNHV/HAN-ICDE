﻿using System;
using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Lessen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/les")]
[Authorize(Roles = UserRole.Auteur)]
public class LesController : Controller
{
    private readonly ILesService _lesService;
    private readonly ILeeruitkomstService _leeruitkomstService;

    public LesController(ILesService lesService, ILeeruitkomstService leeruitkomstService)
    {
        _lesService = lesService;
        _leeruitkomstService = leeruitkomstService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        LesIndexViewModel viewModel = new LesIndexViewModel();
        viewModel.Lessen = await _lesService.AlleUnieke();
        return View("/Views/Auteur/Les/Index.cshtml", viewModel);
    }


    [HttpPost("create")]
    public async Task<IActionResult> MaakLes([FromForm] MaakLesDto request)
    {
        var result = await _lesService.Maak(request);
        if (result is null)
        {
            return BadRequest();
        }
        return Redirect($"get/{result.GroupId}");
    }

    [HttpGet("get/{groupId}")]
    public async Task<IActionResult> BekijkLes([FromRoute] Guid groupId)
    {
        var lesMetEerdereVersies = await _lesService.HaalLessenOpMetEerdereVersies(groupId);
        if (lesMetEerdereVersies is null)
        {
            return NotFound();
        }
        return View("/Views/Auteur/Les/BekijkLes.cshtml", new BekijkLesViewModel()
        {
            Les = lesMetEerdereVersies.Les,
            LesList = lesMetEerdereVersies.LesList,
            LesLeeruitkomsten = lesMetEerdereVersies.LesLeeruitkomsten,
            BeschrikbareLeeruitkomsten = await _leeruitkomstService.AlleUnieke()
        });
    }

    [HttpGet("{groupId}/bekijkversie/{versionId}")]
    public async Task<IActionResult> BekijkVersie([FromRoute] Guid groupId, [FromRoute] int versionId)
    {
        LesDto? les = await _lesService.BekijkVersie(groupId, versionId);
        if (les is null)
        {
            return BadRequest();
        }

        return View("/Views/Auteur/Les/BekijkVersie.cshtml", les);
    }

    [HttpGet("{groupId}/kopie/{versionId}")]
    public async Task<IActionResult> KopieVersie([FromRoute] Guid groupId, [FromRoute] int versionId)
    {
        Guid les = await _lesService.MaakKopie(groupId, versionId);
        if (les == Guid.Empty)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/les/get/{groupId}");
    }

    [HttpGet("delete/{groupId}/{versionId}")]
    public async Task<IActionResult> VerwijderLes([FromRoute] Guid groupId, [FromRoute] int versionId)
    {
        var result = await _lesService.VerwijderVersie(groupId, versionId);
        if (result == false)
            return BadRequest();

        return Redirect("/auteur/les");
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateLes([FromForm] UpdateLesDto request)
    {
        var result = await _lesService.Update(request);
        if (!result)
            return BadRequest();

        return Redirect($"/auteur/les/get/{request.GroupId}");
    }

    [HttpGet("koppelluk/{lesGroupId}/{lukGroupId}")]
    public async Task<IActionResult> KoppelLuk([FromRoute] Guid lesGroupId, [FromRoute] Guid lukGroupId)
    {
        var result = await _lesService.KoppelLukAanLes(lesGroupId, lukGroupId);
        if (!result)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/les/get/{lesGroupId}");
    }

    [HttpGet("ontkoppelluk/{lesGroupId}/{lukGroupId}")]
    public async Task<IActionResult> OntkoppelLuk([FromRoute] Guid lesGroupId, [FromRoute] Guid lukGroupId)
    {
        var result = await _lesService.OntkoppelLukAanLes(lesGroupId, lukGroupId);
        if (!result)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/les/get/{lesGroupId}");
    }
}
