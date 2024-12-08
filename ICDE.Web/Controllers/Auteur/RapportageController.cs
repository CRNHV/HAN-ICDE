using System;
using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/rapportage")]
[Authorize(Roles = UserRole.Auteur)]
public class RapportageController : ControllerBase
{
    private readonly IRapportageService _rapportageService;

    public RapportageController(IRapportageService rapportageService)
    {
        _rapportageService = rapportageService;
    }

    [HttpGet("generate/{opleidingGuid}")]
    public async Task<IActionResult> GenerateReport([FromRoute] Guid opleidingGuid)
    {
        var result = await _rapportageService.ValidateOpleiding(opleidingGuid);
        return Ok();
    }
}
