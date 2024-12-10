using System;
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

    /// <summary>
    /// UC6
    /// </summary>
    /// <returns></returns>
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        LesIndexViewModel viewModel = new LesIndexViewModel();
        viewModel.Lessen = await _lesService.Allemaal();
        return View("/Views/Auteur/Les/Index.cshtml", viewModel);
    }


    /// <summary>
    /// UC6
    /// </summary>
    /// <returns></returns>    
    [HttpPost("create")]
    public async Task<IActionResult> MaakLes([FromForm] MaakLesViewModel request)
    {
        LesDto les = await _lesService.Maak(request.Naam, request.Beschrijving);
        if (les is null)
        {
            return BadRequest();
        }
        return Redirect($"get/{les.GroupId}");
    }

    /// <summary>
    /// UC6
    /// </summary>
    /// <returns></returns>
    [HttpGet("get/{groupId}")]
    public async Task<IActionResult> BekijkLes([FromRoute] Guid groupId)
    {
        LesMetEerdereVersies lmev = await _lesService.HaalLessenOpMetEerdereVersies(groupId);
        if (lmev is null)
        {
            return NotFound();
        }
        return View("/Views/Auteur/Les/BekijkLes.cshtml", new BekijkLesViewModel()
        {
            Les = lmev.Les,
            LesList = lmev.LesList,
            LesLeeruitkomsten = lmev.LesLeeruitkomsten,
            BeschrikbareLeeruitkomsten = await _leeruitkomstService.Allemaal()
        });
    }

    /// <summary>
    /// UC6
    /// </summary>
    /// <returns></returns>
    [HttpGet("get/{groupId}/{versionId}")]
    public async Task<IActionResult> BekijkVersie([FromRoute] Guid groupId, [FromRoute] int versionId)
    {
        return null;
    }

    /// <summary>
    /// UC6
    /// </summary>
    /// <returns></returns>    
    [HttpGet("delete/{groupId}/{versionId}")]
    public async Task<IActionResult> VerwijderLes([FromRoute] Guid groupId, [FromRoute] int versionId)
    {
        var result = await _lesService.VerwijderVersie(groupId, versionId);
        if (result == false)
            return BadRequest();

        return Redirect("/auteur/les");
    }

    /// <summary>
    /// UC6
    /// </summary>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<IActionResult> UpdateLes([FromForm] LesUpdateDto request)
    {
        var result = await _lesService.Update(request);
        if (!result)
            return BadRequest();

        return Redirect($"/auteur/les/get/{request.GroupId}");
    }

    /// <summary>
    /// UC15
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// UC15
    /// </summary>
    /// <returns></returns>
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
