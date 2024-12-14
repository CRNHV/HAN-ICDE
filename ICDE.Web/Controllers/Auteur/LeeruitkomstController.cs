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

    /// <summary>
    /// UC14
    /// </summary>
    /// <returns></returns>
    [HttpPost("maak")]
    [HttpGet("maak")]
    public async Task<IActionResult> MaakLeeruitkomst([FromForm] MaakLeeruitkomstDto request)
    {
        if (IsRequestMethod("POST"))
        {
            LeeruitkomstDto result = await _leeruitkomstService.Maak(request);
            if (result is null)
                return BadRequest();
        }

        return View("/Views/Auteur/Leeruitkomst/MaakLeeruitkomst.cshtml");
    }

    [HttpGet("bekijkalle")]
    public async Task<IActionResult> BekijkLeeruitkomsten()
    {
        List<LeeruitkomstDto> leeruitkomsten = await _leeruitkomstService.AlleUnieke();
        return View("/Views/Auteur/Leeruitkomst/BekijkLeeruitkomsten.cshtml", leeruitkomsten);
    }

    /// <summary>
    /// UC14
    /// </summary>
    /// <returns></returns>
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
    public async Task<IActionResult> BekijkLeeruitkomst([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        var leeruitkomst = await _leeruitkomstService.BekijkVersie(groupId, versieId);
        if (leeruitkomst is null)
        {
            return NotFound();
        }
        return View("/Views/Auteur/Leeruitkomst/BekijkLeeruitkomst.cshtml", new LeeruitkomstMetEerdereVersiesDto()
        {
            Leeruitkomst = leeruitkomst,
        });

    }

    /// <summary>
    /// UC14
    /// </summary>
    /// <returns></returns>
    [HttpGet("delete/{groupId}/{versieId}")]
    public async Task<IActionResult> DeleteLeeruitkosmt([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        var result = await _leeruitkomstService.VerwijderVersie(groupId, versieId);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect($"/auteur/leeruitkomst/bekijkalle");
    }

    /// <summary>
    /// UC14
    /// </summary>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<IActionResult> UpdateLeeruitkomst([FromForm] UpdateLeeruitkomstDto request)
    {
        if (IsRequestMethod("POST"))
        {
            bool result = await _leeruitkomstService.Update(request);
            if (result is false)
                return BadRequest();

            return Redirect($"/auteur/leeruitkomst/bekijk/{request.GroupId}");
        }

        return Redirect("/auteur/leeruitkomst/bekijkalle");
    }
}