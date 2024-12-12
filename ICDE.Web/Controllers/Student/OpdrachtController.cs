using System;
using System.Threading.Tasks;
using ICDE.Data.Entities;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Student;

[Route("student/opdracht")]
[Authorize(Roles = UserRole.Student)]
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
        var opdrachten = await _opdrachtService.Allemaal();
        return View("/Views/Student/Opdracht/Index.cshtml", opdrachten);
    }

    [HttpPost("leverin")]
    public async Task<IActionResult> LeverOpdrachtIn([FromForm] LeverOpdrachtInDto request)
    {
        var userId = GetUserIdFromClaims();
        if (userId is null)
        {
            return BadRequest();
        }

        var result = await _ingeleverdeOpdrachtService.LeverOpdrachtIn(userId.Value, request);
        if (!result)
        {
            return BadRequest();
        }

        return Redirect("/student/opdracht/index");
    }

    [HttpGet("bekijk/{groupId}")]
    public async Task<IActionResult> BekijkOpdracht([FromRoute] Guid groupId)
    {
        StudentOpdrachtDto? opdracht = await _opdrachtService.HaalStudentOpdrachtDataOp(groupId);
        if (opdracht is null)
        {
            return NotFound();
        }

        return View("/Views/Student/Opdracht/BekijkOpdracht.cshtml", opdracht);
    }
}
