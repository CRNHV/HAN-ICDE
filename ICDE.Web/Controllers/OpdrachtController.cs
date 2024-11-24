using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers;

[Route("opdrachten")]
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
        if (HttpContext.Request.Method == "POST")
        {
            var userId = this.GetUserIdFromClaims();
            if (userId is null)
                return Unauthorized();

            await _opdrachtService.LeverOpdrachtIn(userId.Value, request);
        }

        return View(new LeverOpdrachtInDto()
        {
            OpdrachtId = opdrachtId,
        });
    }

    [HttpGet("{opdrachtId}/inzendingen")]
    public async Task<IActionResult> Inzendingen([FromRoute] int opdrachtId)
    {
        List<IngeleverdeOpdrachtDto> ingeleverdeOpdrachten = await _opdrachtService.HaalInzendingenOp(opdrachtId);
        return View(ingeleverdeOpdrachten);
    }

    /// <summary>
    /// UC2
    /// </summary>
    /// <param name="opdrachtId"></param>
    /// <returns></returns>
    [HttpGet("bekijkalle")]
    public async Task<IActionResult> BekijkAlle()
    {
        List<OpdrachtDto> opdrachten = await _opdrachtService.HaalAlleOp();
        return View(opdrachten);
    }

    /// <summary>
    /// UC2
    /// </summary>
    /// <param name="opdrachtId"></param>
    /// <returns></returns>
    [HttpGet("bekijk/{opdrachtId}")]
    public async Task<IActionResult> BekijkOpdracht([FromRoute] int opdrachtId)
    {
        OpdrachtDto? opdrachtDto = await _opdrachtService.Bekijk(opdrachtId);
        return View(opdrachtDto);
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
    /// UC4, UC5, UC19
    /// </summary>
    /// <param name="beoordelingId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> BekijkBeoordeling([FromRoute] int beoordelingId)
    {
        return null;
    }

    /// <summary>
    /// UC8
    /// </summary>
    /// <returns></returns>
    [HttpGet("maak")]
    [HttpPost("maak")]
    public async Task<IActionResult> MaakOpdracht([FromForm] MaakOpdrachtDto request)
    {
        if (HttpContext.Request.Method == "POST")
        {
            await _opdrachtService.MaakOpdracht(request);
        }

        return View();
    }

    /// <summary>
    /// UC8
    /// </summary>
    /// <returns></returns>
    [HttpGet("verwijder")]
    [HttpDelete("verwijder")]
    public async Task<IActionResult> VerwijderOpdracht()
    {
        return null;
    }

    /// <summary>
    /// UC8
    /// </summary>
    /// <returns></returns>
    [HttpGet("update")]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateOpdracht()
    {
        return null;
    }
}