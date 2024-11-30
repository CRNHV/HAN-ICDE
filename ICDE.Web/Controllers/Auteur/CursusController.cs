using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Cursus;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/cursus")]
public class CursusController : ControllerBase
{
    private readonly ICursusService _cursusService;

    public CursusController(ICursusService cursusService)
    {
        _cursusService = cursusService;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        List<CursusDto> cursussen = await _cursusService.GetAll();

        return View("/Views/Auteur/Cursus/Index.cshtml", cursussen);
    }

    [HttpGet("get/{cursusGroupId}")]
    public async Task<IActionResult> BekijkCursus([FromRoute] Guid cursusGroupId)
    {
        CursusMetPlanningDto cursus = await _cursusService.GetFullCursusByGroupId(cursusGroupId);
        List<CursusDto> eerdereVersies = await _cursusService.GetEarlierVersionsByGroupId(cursusGroupId, cursus.Id);
        return View("/Views/Auteur/Cursus/BekijkCursus.cshtml", new BekijkCursusViewModel()
        {
            Cursus = cursus,
            EerderVersies = eerdereVersies
        });
    }

    /// <summary>
    /// UC17
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public async Task<IActionResult> CheckLeeruitkomstOverzichtVanCursus()
    {
        return View();
    }
}
