using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/leeruitkomst")]
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
            LeeruitkomstDto result = await _leeruitkomstService.MaakLeeruitkomst(request);
            if (result is null)
                return BadRequest();
        }

        return View("/Views/Auteur/Leeruitkomst/MaakLeeruitkomst.cshtml");
    }

    [HttpGet("bekijkalle")]
    public async Task<IActionResult> BekijkLeeruitkomsten()
    {
        List<LeeruitkomstDto> leeruitkomsten = await _leeruitkomstService.GetAll();
        return View("/Views/Auteur/Leeruitkomst/BekijkLeeruitkomsten.cshtml", leeruitkomsten);
    }

    /// <summary>
    /// UC14
    /// </summary>
    /// <returns></returns>
    [HttpGet("bekijk/{groupId}")]
    public async Task<IActionResult> BekijkLeeruitkomst([FromRoute] Guid groupId)
    {
        var leeruitkomst = await _leeruitkomstService.GetEntityWithEarlierVersions(groupId);
        if (leeruitkomst is null)
        {
            return NotFound();
        }
        return View("/Views/Auteur/Leeruitkomst/BekijkLeeruitkomst.cshtml", leeruitkomst);
    }

    [HttpGet("bekijkversie/{groupId}/{versieId}")]
    public async Task<IActionResult> BekijkLeeruitkomst([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        var leeruitkomst = await _leeruitkomstService.GetVersion(groupId, versieId);
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
    public async Task<IActionResult> VerwijderLeeruitkomst()
    {
        return null;
    }

    /// <summary>
    /// UC14
    /// </summary>
    /// <returns></returns>
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