using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services.Interfaces;
using ICDE.Lib.ViewModels;
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

        return View();
    }

    [HttpGet("bekijkalle")]
    public async Task<IActionResult> BekijkLeeruitkomsten()
    {
        List<LeeruitkomstDto> leeruitkomsten = await _leeruitkomstService.GetAll();
        return View(leeruitkomsten);
    }

    /// <summary>
    /// UC14
    /// </summary>
    /// <returns></returns>
    [HttpGet("bekijk/{groupId}")]
    public async Task<IActionResult> BekijkLeeruitkomst([FromRoute] Guid groupId)
    {
        var leeruitkomst = await _leeruitkomstService.GetEntityWithEarlierVersions(groupId);
        return View(leeruitkomst);

    }

    [HttpGet("bekijkversie/{groupId}/{versieId}")]
    public async Task<IActionResult> BekijkLeeruitkomst([FromRoute] Guid groupId, [FromRoute] int versieId)
    {
        LeeruitkomstDto leeruitkomst = await _leeruitkomstService.GetVersion(groupId, versieId);
        return View(new LeeruitkomstMetEerdereVersies()
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

            return Redirect($"/leeruitkomst/bekijk/{result.GroupId}");
        }

        return Redirect("leeruitkomst/bekijkalle");
    }
}