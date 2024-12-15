using System;
using System.Linq;
using System.Threading.Tasks;
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
        var allOpleidingen = await _opleidingService.AlleUnieke();
        var vakken = await _vakService.AlleUnieke();
        var cursus = await _cursusService.AlleUnieke();

        return View("/Views/Auteur/Rapportage/Index.cshtml", new RapportageIndexViewModel()
        {
            Cursussen = cursus,
            Opleidingen = allOpleidingen,
            Vakken = vakken
        });
    }

    [HttpGet("{type}/{groupGuid}/")]
    public async Task<IActionResult> GenereerRapportage([FromRoute] string type, [FromRoute] Guid groupGuid)
    {
        var result = await _rapportageService.GenereerRapportage(type, groupGuid);
        return View("/Views/Auteur/Rapportage/ViewReport.cshtml", new RapportageResultViewModel()
        {
            GroupId = groupGuid,
            Results = result,
            Success = result.All(x => x.Success),
            Type = type
        });
    }

    [HttpGet("{type}/{groupGuid}/download")]
    public async Task<IActionResult> DownloadRapportage([FromRoute] string type, [FromRoute] Guid groupGuid)
    {
        var rapportage = await _rapportageService.ExporteerRapportage(type, groupGuid);
        if (rapportage is null)
        {
            return BadRequest();
        }
        return File(rapportage.Bytes, "application/pdf", "rapportage.pdf");
    }
}
