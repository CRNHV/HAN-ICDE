using System;
using System.Threading.Tasks;
using ICDE.Data.Entities;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Rapportage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/rapportage")]
[Authorize(Roles = UserRole.Auteur)]
public class RapportageController : ControllerBase
{
    private readonly IRapportageService _rapportageService;
    private readonly IOpleidingService _opleidingService;
    private readonly IVakService _vakService;
    private readonly ICursusService _cursusService;

    public RapportageController(IRapportageService rapportageService, IOpleidingService opleidingService, IVakService vakService, ICursusService cursusService)
    {
        _rapportageService = rapportageService;
        _opleidingService = opleidingService;
        _vakService = vakService;
        _cursusService = cursusService;
    }

    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        var allOpleidingen = await _opleidingService.GetAllUnique();
        var vakken = await _vakService.Allemaal();
        var cursus = await _cursusService.GetAll();

        return View("/Views/Auteur/Rapportage/Index.cshtml", new RapportageIndexViewModel()
        {
            Cursussen = cursus,
            Opleidingen = allOpleidingen,
            Vakken = vakken
        });
    }

    [HttpGet("opleiding/{groupGuid}")]
    public async Task<IActionResult> GenerateOpleidingReport([FromRoute] Guid groupGuid)
    {
        var result = await _rapportageService.GenereerRapportVoorOpleiding(groupGuid);
        return View("/Views/Auteur/Rapportage/ViewReport.cshtml", result);
    }

    [HttpGet("vak/{groupGuid}")]
    public async Task<IActionResult> GenerateVakReport([FromRoute] Guid groupGuid)
    {
        var result = await _rapportageService.GenereerRapportVoorVak(groupGuid);
        return View("/Views/Auteur/Rapportage/ViewReport.cshtml", result);
    }

    [HttpGet("cursus/{groupGuid}")]
    public async Task<IActionResult> GenerateCursusReport([FromRoute] Guid groupGuid)
    {
        var result = await _rapportageService.GenereerRapportVoorCursus(groupGuid);
        return View("/Views/Auteur/Rapportage/ViewReport.cshtml", result);
    }
}
