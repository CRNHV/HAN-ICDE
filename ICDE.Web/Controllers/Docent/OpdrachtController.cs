using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Docent;

[Route("docent/opdrachten")]
public class OpdrachtController : ControllerBase
{
    private readonly IOpdrachtService _opdrachtService;

    public OpdrachtController(IOpdrachtService opdrachtService)
    {
        _opdrachtService = opdrachtService;
    }

    /// <summary>
    /// UC2
    /// </summary>
    /// <param name="opdrachtId"></param>
    /// <returns></returns>
    [HttpGet("{opdrachtId}/inzendingen")]
    public async Task<IActionResult> Inzendingen([FromRoute] int opdrachtId)
    {
        List<IngeleverdeOpdrachtDto> ingeleverdeOpdrachten = await _opdrachtService.HaalInzendingenOp(opdrachtId);
        return View(ingeleverdeOpdrachten);
    }

    /// <summary>
    /// UC3
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("inzending/{inzendingId}/beoordeel")]
    [HttpPost("inzending/beoordeel")]
    public async Task<IActionResult> BeoordeelOpdracht([FromRoute] int inzendingId, [FromForm] OpdrachtBeoordelingDto request)
    {
        if (HttpContext.Request.Method == "POST")
        {
            await _opdrachtService.SlaBeoordelingOp(request);
        }

        return View(new OpdrachtBeoordelingDto()
        {
            InzendingId = inzendingId
        });
    }

    /// <summary>
    /// UC5
    /// </summary>
    /// <param name="beoordelingId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> BekijkBeoordeling([FromRoute] int beoordelingId)
    {
        return null;
    }
}
