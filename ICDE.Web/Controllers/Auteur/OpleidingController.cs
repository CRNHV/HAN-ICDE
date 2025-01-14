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

    [HttpGet("ontkoppelvak/{opleidingGroupId}/{vakGroupId}")]
    public async Task<IActionResult> OntkoppelVakVanOpleiding([FromRoute] Guid opleidingGroupId, [FromRoute] Guid vakGroupId)
    {
        bool result = await _opleidingService.OntkoppelVakVanOpleiding(opleidingGroupId, vakGroupId);
        if (!result)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/opleiding/bekijk/{opleidingGroupId}");
    }

    [HttpPost("create")]
    public async Task<IActionResult> MaakOpleiding([FromForm] MaakOpleidingDto request)
    {
        var result = await _opleidingService.Maak(request);
        if (result is null)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/opleiding/bekijk/{result.GroupId}");
    }

    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        List<OpleidingDto> opleidingen = await _opleidingService.AlleUnieke();
        return View("/Views/Auteur/Opleiding/Index.cshtml", opleidingen);
    }

    [HttpGet("bekijk/{groupId}")]
    public async Task<IActionResult> BekijkOpleiding([FromRoute] Guid groupId)
    {
        var opleidingMetVersies = await _opleidingService.ZoekOpleidingMetEerdereVersies(groupId);
        if (opleidingMetVersies is null)
        {
            return NotFound();
        }
        var vakken = await _vakService.AlleUnieke();

        var viewModel = new BekijkOpleidingViewModel()
        {
            Opleiding = opleidingMetVersies.OpleidingDto,
            EerdereVersies = opleidingMetVersies.EerdereVersies,
            BeschikbareVakken = vakken,
        };
        return View("/Views/Auteur/Opleiding/BekijkOpleiding.cshtml", viewModel);
    }

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

    [HttpGet("verwijder/{groupId}/{versie}")]
    public async Task<IActionResult> VerwijderOpleiding([FromRoute] Guid groupId, [FromRoute] int versie)
    {
        bool result = await _opleidingService.VerwijderVersie(groupId, versie);
        if (!result)
        {
            return BadRequest();
        }

        return RedirectToAction("Index");
    }

    [HttpPost("Update")]
    public async Task<IActionResult> UpdateOpleiding([FromForm] UpdateOpleidingDto request)
    {
        var result = await _opleidingService.Update(request);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/opleiding/bekijk/{request.GroupId}");
    }

    [HttpGet("{opleidingGroupId}/copy")]
    public async Task<IActionResult> KopieerOpleiding([FromRoute] Guid opleidingGroupId)
    {
        var resultGuid = await _opleidingService.MaakKopie(opleidingGroupId, 0);
        if (resultGuid == Guid.Empty)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/opleiding/bekijk/{resultGuid}");
    }
}