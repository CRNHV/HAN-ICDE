using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.OpdrachtInzending;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Docent;

[Route("docent/opdrachtinzending")]
[Authorize(Roles = UserRole.Docent)]
public class OpdrachtInzendingController : ControllerBase
{
    private readonly IIngeleverdeOpdrachtService _ingeleverdeService;

    public OpdrachtInzendingController(IIngeleverdeOpdrachtService ingeleverdeService)
    {
        _ingeleverdeService = ingeleverdeService;
    }

    [HttpGet("{inzendingId}")]
    public async Task<IActionResult> BekijkInzending([FromRoute] int inzendingId)
    {
        var inzending = await _ingeleverdeService.HaalInzendingDataOp(inzendingId);
        if (inzending is null)
        {
            return NotFound();
        }

        return View("/Views/Docent/OpdrachtInzending/BekijkInzending.cshtml", new BekijkInzendingViewModel()
        {
            Inzending = inzending,
        });
    }

    [HttpPost("beoordeel")]
    public async Task<IActionResult> VoegBeoordelingToe([FromForm] OpdrachtBeoordelingDto request)
    {
        var result = await _ingeleverdeService.SlaBeoordelingOp(request);
        if (result == false)
        {
            return BadRequest();
        }
        return Redirect($"/docent/opdrachtinzending/{request.InzendingId}");
    }
}
