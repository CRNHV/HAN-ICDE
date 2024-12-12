using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/leeruitkomst")]
[Authorize(Roles = UserRole.Auteur)]
public class LeeruitkomstController : ControllerBase
{
    private readonly ILeeruitkomstService _leeruitkomstService;

    public LeeruitkomstController(ILeeruitkomstService leeruitkomstService)
    {
        _leeruitkomstService = leeruitkomstService;
    }

    [HttpPost("maak")]
    [HttpGet("maak")]
    public async Task<IActionResult> MaakLeeruitkomst([FromForm] MaakLeeruitkomstDto request)
    {
        if (IsRequestMethod("POST"))
        {
            LeeruitkomstDto result = await _leeruitkomstService.MaakLeeruitkomst(request);
            if (result is null)
                return BadRequest();
        }

        return View("/Views/Auteur/Leeruitkomst/MaakLeeruitkomst.cshtml");
    }

    [HttpGet("bekijkalle")]
    public async Task<IActionResult> BekijkLeeruitkomsten()
    {
        List<LeeruitkomstDto> leeruitkomsten = await _leeruitkomstService.Allemaal();
        return View("/Views/Auteur/Leeruitkomst/BekijkLeeruitkomsten.cshtml", leeruitkomsten);
    }

    [HttpGet("bekijk/{groupId}")]
    public async Task<IActionResult> BekijkLeeruitkomst([FromRoute] Guid groupId)
    {
        var leeruitkomst = await _leeruitkomstService.HaalOpMetEerdereVersies(groupId);
        if (leeruitkomst is null)
        {
            return NotFound();
        }
        return View("/Views/Auteur/Leeruitkomst/BekijkLeeruitkomst.cshtml", leeruitkomst);
    }

    [HttpGet("bekijkversie/{groupId}/{versieId}")]
    public async Task<IActionResult> BekijkVersie([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        var leeruitkomst = await _leeruitkomstService.HaalVersieOp(groupId, versieId);
        if (leeruitkomst is null)
        {
            return NotFound();
        }
        return View("/Views/Auteur/Leeruitkomst/BekijkLeeruitkomstVersie.cshtml", leeruitkomst);

    }

    [HttpGet("{groupId}/kopie/{versieId}")]
    public async Task<IActionResult> MaakKopieVanVersie([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        Guid leeruitkomst = await _leeruitkomstService.MaakKopieVanVersie(groupId, versieId);
        if (leeruitkomst == Guid.Empty)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/leeruitkomst/bekijk/{leeruitkomst}");

    }

    [HttpGet("delete/{groupId}/{versieId}")]
    public async Task<IActionResult> DeleteLeeruitkosmt([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        var result = await _leeruitkomstService.Verwijder(groupId, versieId);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/leeruitkomst/bekijkalle");
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateLeeruitkomst([FromForm] LukUpdateDto request)
    {
        if (IsRequestMethod("POST"))
        {
            LeeruitkomstDto result = await _leeruitkomstService.UpdateLeeruitkomst(request);
            if (result is null)
                return BadRequest();

            return Redirect($"/auteur/leeruitkomst/bekijk/{result.GroupId}");
        }

        return Redirect("/auteur/leeruitkomst/bekijkalle");
    }
}