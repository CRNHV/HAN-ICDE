using System.Threading.Tasks;
using ICDE.Lib.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers;

[Route("les")]
[Authorize(Roles = UserRole.Auteur)]
public class LesController : Controller
{
    /// <summary>
    /// UC6
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HttpPost]
    public async Task<IActionResult> MaakLes()
    {
        return null;
    }

    /// <summary>
    /// UC6
    /// </summary>
    /// <returns></returns>
    [HttpGet]

    public async Task<IActionResult> BekijkLes()
    {
        return null;
    }

    /// <summary>
    /// UC6
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HttpDelete]
    public async Task<IActionResult> VerwijderLes()
    {
        return null;
    }

    /// <summary>
    /// UC6
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HttpPut]
    public async Task<IActionResult> UpdateLes()
    {
        return null;
    }

}
