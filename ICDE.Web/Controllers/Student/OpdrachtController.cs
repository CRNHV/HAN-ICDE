using System.Threading.Tasks;
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

    public OpdrachtController(IOpdrachtService opdrachtService)
    {
        _opdrachtService = opdrachtService;
    }

    /// <summary>
    /// UC1
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("{opdrachtId}/leverin")]
    [HttpPost("{opdrachtId}/leverin")]
    public async Task<IActionResult> LeverOpdrachtIn([FromRoute] int opdrachtId, [FromForm] LeverOpdrachtInDto request)
    {
        //if (HttpContext.Request.Method == "POST")
        //{
        //    var userId = this.GetUserIdFromClaims();
        //    if (userId is null)
        //        return Unauthorized();

        //    await _opdrachtService.LeverOpdrachtIn(userId.Value, request);
        //}

        //return View(new LeverOpdrachtInDto()
        //{
        //    OpdrachtId = opdrachtId,
        //});

        return Ok();
    }

    /// <summary>
    /// UC4, UC19
    /// </summary>
    /// <param name="beoordelingId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> BekijkBeoordeling([FromRoute] int beoordelingId)
    {
        return null;
    }
}
