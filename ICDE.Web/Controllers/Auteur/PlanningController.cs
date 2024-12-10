using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Planning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/planning")]
[Authorize(Roles = UserRole.Auteur)]
public class PlanningController : ControllerBase
{
    private readonly IPlanningService _planningService;
    private readonly ICursusService _cursusService;
    private readonly IOpdrachtService _opdrachtService;
    private readonly ILesService _lesService;

    public PlanningController(
        IPlanningService planningService,
        ICursusService cursusService,
        IOpdrachtService opdrachtService,
        ILesService lesService)
    {
        _planningService = planningService;
        _cursusService = cursusService;
        _opdrachtService = opdrachtService;
        _lesService = lesService;
    }

    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        List<PlanningZonderItemsDto> plannings = await _planningService.GetAll();
        return View("/Views/Auteur/Planning/Index.cshtml", plannings);
    }

    public async Task<IActionResult> MaakPlanning()
    {
        return null;
    }

    [HttpGet("bekijk/{planningId}")]
    public async Task<IActionResult> BekijkPlanning([FromRoute] int planningId)
    {
        var planning = await _planningService.GetById(planningId);
        if (planning is null)
        {
            return NotFound();
        }
        var cursussen = await _cursusService.GetAll();
        var opdrachten = await _opdrachtService.GetAll();
        var lessen = await _lesService.GetAll();
        var lessenInPlanning = await _planningService.GetLessonsForPlanning(planningId);

        return View("/Views/Auteur/Planning/ViewPlanning.cshtml", new BekijkPlanningViewModel()
        {
            Planning = planning,
            Cursussen = cursussen,
            Lessen = lessen,
            Opdrachten = opdrachten,
            LessenInPlanning = lessenInPlanning,
        });
    }

    [HttpGet("{planningId}/voegcursustoe/{cursusGroupId}")]
    public async Task<IActionResult> VoegToeAanCursus([FromRoute] int planningId, [FromRoute] Guid cursusGroupId)
    {
        var result = await _cursusService.VoegPlanningToeAanCursus(cursusGroupId, planningId);
        if (result is false)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/planning/bekijk/{planningId}");
    }

    public async Task<IActionResult> VerwijderPlanning()
    {
        return null;
    }

    public async Task<IActionResult> UpdatePlanning()
    {
        return null;
    }

    /// <summary>
    /// UC22
    /// </summary>
    /// <returns></returns>
    [HttpGet("{planningId}/voegopdrachttoe/{groupId}")]
    public async Task<IActionResult> VoegOpdrachtToe([FromRoute] int planningId, [FromRoute] Guid groupId)
    {
        var dto = await _planningService.VoegOpdrachtToe(planningId, groupId);
        if (dto is null)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/planning/bekijk/{planningId}");
    }

    /// <summary>
    /// UC22
    /// </summary>
    /// <returns></returns>
    [HttpGet("{planningId}/voeglestoe/{groupId}")]
    public async Task<IActionResult> VoegLesTOe([FromRoute] int planningId, [FromRoute] Guid groupId)
    {
        var dto = await _planningService.VoegLesToe(planningId, groupId);
        if (dto is null)
        {
            return BadRequest();
        }
        return Redirect($"/auteur/planning/bekijk/{planningId}");
    }
}
