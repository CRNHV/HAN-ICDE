using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers;

[Route("leeruitkomst")]
public class LeeruitkomstController : ControllerBase
{
    private readonly ILeeruitkomstService _leeruitkomstService;

    public LeeruitkomstController(ILeeruitkomstService leeruitkomstService)
    {
        _leeruitkomstService = leeruitkomstService;
    }

    /// <summary>
    /// UC14
    /// </summary>
    /// <returns></returns>
    [HttpPost("maak")]
    [HttpGet("maak")]
    public async Task<IActionResult> MaakLeeruitkomst([FromForm] MaakLeeruitkomstDto request)
    {
        if (IsRequestMethod("POST"))
        {
            LeeruitkomstDto result = await _leeruitkomstService.MaakLeeruitkomst(request);
            if (result is null)
                return BadRequest();
        }

        return View();
    }

    /// <summary>
    /// UC14
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> BekijkLeeruitkomst()
    {
        return null;
    }

    /// <summary>
    /// UC14
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> VerwijderLeeruitkomst()
    {
        return null;
    }

    /// <summary>
    /// UC14
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> UpdateLeeruitkomst([FromForm] LeeruitkomstDto request)
    {
        if (IsRequestMethod("POST"))
        {
            LeeruitkomstDto result = await _leeruitkomstService.UpdateLeeruitkomst(request);
            if (result is null)
                return BadRequest();
        }

        return View();
    }

    /// <summary>
    /// UC15
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> KoppelLeeruitkomst()
    {
        return null;
    }

    /// <summary>
    /// UC16
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> ControlleerLeeruitkomsten()
    {
        return null;
    }

    /// <summary>
    /// UC17
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GenereerLeeruitkomstOverzicht()
    {
        return null;
    }
}