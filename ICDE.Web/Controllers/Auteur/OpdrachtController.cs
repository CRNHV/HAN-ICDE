using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace ICDE.Web.Controllers.Auteur;


[Route("auteur/opdracht")]
public class OpdrachtController : ControllerBase
{
    private readonly IOpdrachtService _opdrachtService;

    public OpdrachtController(IOpdrachtService opdrachtService)
    {
        _opdrachtService = opdrachtService;
    }

    [HttpGet("bekijkalle")]
    public async Task<IActionResult> BekijkAlle()
    {
        List<OpdrachtDto> opdrachten = await _opdrachtService.HaalAlleOp();
        return View("/Views/Auteur/Opdracht/BekijkAlle.cshtml", opdrachten);
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

        return View("/Views/Auteur/Opdracht/MaakOpdracht.cshtml");
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

    /// <summary>
    /// UC15
    /// </summary>
    /// <returns></returns>
    [HttpGet("koppelluk/{lesGroupId}/{lukGroupId}")]
    public async Task<IActionResult> KoppelLuk([FromRoute] Guid lesGroupId, [FromRoute] Guid lukGroupId)
    {
        return View();
    }

    /// <summary>
    /// UC15
    /// </summary>
    /// <returns></returns>
    [HttpGet("ontkoppelluk/{lesGroupId}/{lukGroupId}")]
    public async Task<IActionResult> OntkoppelLuk([FromRoute] Guid lesGroupId, [FromRoute] Guid lukGroupId)
    {
        return View();
    }
}
