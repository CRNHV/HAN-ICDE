using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.Vak;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Vakken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/vak")]
[Authorize(Roles = UserRole.Auteur)]
public class VakController : Controller
{
    private readonly IVakService _vakService;
    private readonly ILeeruitkomstService _leeruitkomstService;
    private readonly ICursusService _cursusService;

    public VakController(IVakService vakService, ILeeruitkomstService leeruitkomstService, ICursusService cursusService)
    {
        _vakService = vakService;
        _leeruitkomstService = leeruitkomstService;
        _cursusService = cursusService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<VakDto> vakken = await _vakService.AlleUnieke();
        return View("Views/Auteur/Vak/Index.cshtml", new VakIndexViewModel()
        {
            Vakken = vakken
        });
    }

    /// <summary>
    /// UC9
    /// </summary>
    /// <returns></returns>
    [HttpPost("maak")]
    public async Task<IActionResult> MaakVak([FromForm] MaakVakViewModel request)
    {
        //Guid groupId = await _vakService.MaakVak(request.Naam, request.Beschrijving);
        //if (groupId == Guid.Empty)
        //{
        //    return BadRequest();
        //}

        //return Redirect($"get/{groupId}");
    }

    /// <summary>
    /// UC9
    /// </summary>
    /// <returns></returns>
    [HttpGet("get/{vakGroupId}")]
    public async Task<IActionResult> BekijkVak([FromRoute] Guid vakGroupId)
    {
        var vak = await _vakService.HaalVolledigeVakDataOp(vakGroupId);
        if (vak is null)
        {
            return NotFound();
        }

        var luks = await _leeruitkomstService.AlleUnieke();
        var cursussen = await _cursusService.AlleUnieke();

        return View("Views/Auteur/Vak/BekijkVak.cshtml", new BekijkVakViewModel()
        {
            Vak = vak,
            Cursussen = cursussen,
            Leeruitkomsten = luks,
            EerdereVersies = vak.EerdereVersies
        });
    }

    /// <summary>
    /// UC9
    /// </summary>
    /// <returns></returns>

    [HttpGet("delete/{vakGroupId}/{vakVersieId}")]
    public async Task<IActionResult> VerwijderVak([FromRoute] Guid vakGroupId, [FromRoute] int vakVersie)
    {
        var result = await _vakService.VerwijderVersie(vakGroupId, vakVersie);
        if (!result)
            return BadRequest();

        return Redirect($"/auteur/vak/get/{vakGroupId}");
    }

    /// <summary>
    /// UC9
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> UpdateVak([FromForm] UpdateVakDto request)
    {
        var result = await _vakService.Update(request);
        if (!result)
            return BadRequest();

        return Redirect($"/auteur/vak/get/{request.GroupId}");
    }

    /// <summary>
    /// UC13
    /// </summary>
    /// <returns></returns>
    [HttpGet("koppelcursus/{vakGroupId}/{cursusGroupId}")]
    public async Task<IActionResult> KoppelCursus([FromRoute] Guid vakGroupId, [FromRoute] Guid cursusGroupId)
    {
        var result = await _vakService.KoppelCursus(vakGroupId, cursusGroupId);
        if (result is false)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/vak/get/{vakGroupId}");
    }

    /// <summary>
    /// UC13
    /// </summary>
    /// <returns></returns>
    [HttpGet("koppelluk/{vakGroupId}/{lukGroupId}")]
    public async Task<IActionResult> KoppelLeeruitkomst([FromRoute] Guid vakGroupId, [FromRoute] Guid lukGroupId)
    {
        var result = await _vakService.KoppelLeeruitkomst(vakGroupId, lukGroupId);
        if (result is false)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/vak/get/{vakGroupId}");
    }
}