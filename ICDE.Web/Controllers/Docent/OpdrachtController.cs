using System;
using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Opdrachten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Docent;

[Route("docent/opdracht")]
[Authorize(Roles = UserRole.Docent)]
public class OpdrachtController : ControllerBase
{
    private readonly IOpdrachtService _opdrachtService;
    private readonly IIngeleverdeOpdrachtService _ingeleverdeOpdrachtService;

    public OpdrachtController(IOpdrachtService opdrachtService, IIngeleverdeOpdrachtService ingeleverdeOpdrachtService)
    {
        _opdrachtService = opdrachtService;
        _ingeleverdeOpdrachtService = ingeleverdeOpdrachtService;
    }

    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        var opdrachten = await _opdrachtService.AlleUnieke();
        return View("/Views/Docent/Opdracht/Index.cshtml", new OpdrachtenIndexViewModel()
        {
            Opdrachten = opdrachten,
        });
    }

    /// <summary>
    /// UC2
    /// </summary>
    /// <param name="opdrachtGroupId"></param>
    /// <returns></returns>
    [HttpGet("bekijk/{opdrachtGroupId}")]
    public async Task<IActionResult> BekijkOpdracht([FromRoute] Guid opdrachtGroupId)
    {
        var opdracht = await _opdrachtService.NieuwsteVoorGroepId(opdrachtGroupId);
        if (opdracht is null)
        {
            return NotFound();
        }

        var inzendingen = await _ingeleverdeOpdrachtService.HaalInzendingenOp(opdrachtGroupId);

        return View("/Views/Docent/Opdracht/BekijkOpdracht.cshtml", new BekijkOpdrachtViewModel()
        {
            Opdracht = opdracht,
            Inzendingen = inzendingen,
        });
    }
}
