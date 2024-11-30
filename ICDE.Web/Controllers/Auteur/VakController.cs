using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Dto.Vak;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Vakken;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/vak")]
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
        List<VakDto> vakken = await _vakService.GetAll();
        return View("/Views/Auteur/Vak/Index.cshtml", new VakIndexViewModel()
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
        Guid groupId = await _vakService.CreateCourse(request.Naam, request.Beschrijving);
        return Redirect($"get/{groupId}");
    }

    /// <summary>
    /// UC9
    /// </summary>
    /// <returns></returns>
    [HttpGet("get/{vakGroupId}")]
    public async Task<IActionResult> BekijkVak([FromRoute] Guid vakGroupId)
    {
        var luks = await _leeruitkomstService.GetAll();
        var cursussen = await _cursusService.GetAll();
        var vak = await _vakService.GetByGroupId(vakGroupId);

        return View("/Views/Auteur/Vak/BekijkVak.cshtml", new BekijkVakViewModel()
        {
            Vak = vak,
            Cursussen = cursussen,
            Leeruitkomsten = luks
        });
    }

    /// <summary>
    /// UC9
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> VerwijderVak()
    {
        return View();
    }

    /// <summary>
    /// UC9
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> UpdateVak()
    {
        return View();
    }

    /// <summary>
    /// UC13
    /// </summary>
    /// <returns></returns>
    [HttpGet("koppelcursus/{vakGroupId}/{cursusGroupId}")]
    public async Task<IActionResult> KoppelCursus([FromRoute] Guid vakGroupId, [FromRoute] Guid cursusGroupId)
    {
        await _vakService.KoppelCursus(vakGroupId, cursusGroupId);
        return Redirect($"/auteur/vak/get/{vakGroupId}");
    }

    /// <summary>
    /// UC13
    /// </summary>
    /// <returns></returns>
    [HttpGet("koppelluk/{vakGroupId}/{lukGroupId}")]
    public async Task<IActionResult> KoppelLeeruitkomst([FromRoute] Guid vakGroupId, [FromRoute] Guid lukGroupId)
    {
        await _vakService.KoppelLeeruitkomst(vakGroupId, lukGroupId);
        return Redirect($"/auteur/vak/get/{vakGroupId}");
    }
}