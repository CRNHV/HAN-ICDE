using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/planning")]
public class PlanningController : ControllerBase
{
    public async Task<IActionResult> MaakPlanning()
    {
        return null;
    }

    public async Task<IActionResult> BekijkPlanning()
    {
        return null;
    }

    public async Task<IActionResult> VerwijderPlanning()
    {
        return null;
    }

    public async Task<IActionResult> UpdatePlanning()
    {
        return null;
    }

    /// <summary>
    /// UC22
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> VoegOnderwijsonderdeelToeAanPlanning()
    {
        return null;
    }

    /// <summary>
    /// UC22
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> VerwijderOnderwijsOnderdeelVanPlanning()
    {
        return null;
    }
}
