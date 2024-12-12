using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Student;

[Route("student/cijfer")]
[Authorize(Roles = UserRole.Student)]
public class CijferController : ControllerBase
{

    private readonly IOpdrachtBeoordelingService _opdrachtBeoordelingService;

    public CijferController(IOpdrachtBeoordelingService opdrachtBeoordelingService)
    {
        _opdrachtBeoordelingService = opdrachtBeoordelingService;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> Index([FromRoute] Guid cursusGroupId)
    {
        var userId = GetUserIdFromClaims();
        if (userId is null)
        {
            return BadRequest();
        }

        List<OpdrachtMetBeoordelingDto> beoordelingen = await _opdrachtBeoordelingService.HaalBeoordelingenOpVoorUser(userId);

        return View("/Views/Student/Cijfer/Index.cshtml", beoordelingen);
    }
}
