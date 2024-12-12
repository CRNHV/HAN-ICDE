﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Opleiding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/opleiding")]
[Authorize(Roles = UserRole.Auteur)]
public class OpleidingController : Controller
{
    private readonly IOpleidingService _opleidingService;
    private readonly IVakService _vakService;

    public OpleidingController(IOpleidingService opleidingService, IVakService vakService)
    {
        _opleidingService = opleidingService;
        _vakService = vakService;
    }


    /// <summary>
    /// UC10
    /// </summary>
    /// <returns></returns>
    [HttpGet("koppelvak/{opleidingGroupId}/{vakGroupId}")]
    public async Task<IActionResult> KoppelVakAanOpleiding([FromRoute] Guid opleidingGroupId, [FromRoute] Guid vakGroupId)
    {
        bool result = await _opleidingService.KoppelVakAanOpleiding(opleidingGroupId, vakGroupId);
        if (!result)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/opleiding/bekijk/{opleidingGroupId}");
    }

    /// <summary>
    /// UC11
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> MaakOpleiding([FromForm] CreateOpleiding request)
    {
        var result = await _opleidingService.Maak(request);
        if (result is null)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/opleiding/bekijk/{result.GroupId}");
    }

    /// <summary>
    /// UC11
    /// </summary>
    /// <returns></returns>
    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        List<OpleidingDto> opleidingen = await _opleidingService.HaalUniekeOp();
        return View("/Views/Auteur/Opleiding/Index.cshtml", opleidingen);
    }

    /// <summary>
    /// UC11
    /// </summary>
    /// <returns></returns>
    [HttpGet("bekijk/{groupId}")]
    public async Task<IActionResult> BekijkOpleiding([FromRoute] Guid groupId)
    {
        var opleidingMetVersies = await _opleidingService.ZoekOpleidingMetEerdereVersies(groupId);
        if (opleidingMetVersies is null)
        {
            return NotFound();
        }
        var vakken = await _vakService.Allemaal();

        var viewModel = new BekijkOpleidingViewModel()
        {
            Opleiding = opleidingMetVersies.OpleidingDto,
            EerdereVersies = opleidingMetVersies.EerdereVersies,
            BeschikbareVakken = vakken,
        };
        return View("/Views/Auteur/Opleiding/BekijkOpleiding.cshtml", viewModel);
    }

    /// <summary>
    /// UC11
    /// </summary>
    /// <returns></returns>    
    [HttpGet("{groupId}/bekijkversie/{versie}")]
    public async Task<IActionResult> BekijkVersie([FromRoute] Guid groupId, [FromRoute] int versie)
    {
        var opleiding = await _opleidingService.BekijkVersie(groupId, versie);
        if (opleiding is null)
        {
            return NotFound();
        }

        return View("/Views/Auteur/Opleiding/BekijkVersie.cshtml", opleiding);
    }

    /// <summary>
    /// UC11
    /// </summary>
    /// <returns></returns>    
    [HttpGet("verwijder/{groupId}/{versie}")]
    public async Task<IActionResult> VerwijderOpleiding([FromRoute] Guid groupId, [FromRoute] int versie)
    {
        bool result = await _opleidingService.Verwijder(groupId, versie);
        if (!result)
        {
            return BadRequest();
        }

        return RedirectToAction("Index");
    }

    /// <summary>
    /// UC11
    /// </summary>
    /// <returns></returns>
    [HttpPost("Update")]
    public async Task<IActionResult> UpdateOpleiding([FromForm] UpdateOpleiding request)
    {
        var result = await _opleidingService.Update(request);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/opleiding/bekijk/{request.GroupId}");
    }

    /// <summary>
    /// UC12
    /// </summary>
    /// <returns></returns>
    [HttpGet("{opleidingGroupId}/copy")]
    public async Task<IActionResult> KopieerOpleiding([FromRoute] Guid opleidingGroupId)
    {
        var resultGuid = await _opleidingService.Kopie(opleidingGroupId);
        if (resultGuid == Guid.Empty)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/opleiding/bekijk/{resultGuid}");
    }

    [HttpGet("{vakGroupId}/kopie/{vakVersie}")]
    public async Task<IActionResult> Kopie([FromRoute] Guid vakGroupId, [FromRoute] int vakVersie)
    {
        Guid groupId = await _opleidingService.MaakKopie(vakGroupId, vakVersie);
        if (groupId == Guid.Empty)
            return BadRequest();

        return Redirect($"/auteur/opleiding/bekijk/{groupId}");
    }

    [HttpGet("delete/{vakGroupId}/{vakVersie}")]
    public async Task<IActionResult> VerwijderVak([FromRoute] Guid vakGroupId, [FromRoute] int vakVersie)
    {
        var result = await _opleidingService.VerwijderVersie(vakGroupId, vakVersie);
        if (!result)
            return BadRequest();

        return Redirect($"/auteur/opleiding/bekijk/{vakGroupId}");
    }
}