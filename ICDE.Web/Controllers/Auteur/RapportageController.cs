using System;
using System.Linq;
using System.Threading.Tasks;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Validator;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers.Auteur;

[Route("auteur/rapportage")]
public class RapportageController : ControllerBase
{
    private readonly IOpleidingRepository _opleidingRepository;

    public RapportageController(IOpleidingRepository opleidingRepository)
    {
        _opleidingRepository = opleidingRepository;
    }

    [HttpGet("generate/{opleidingGuid}")]
    public async Task<IActionResult> GenerateReport([FromRoute] Guid opleidingGuid)
    {
        var opleiding = await _opleidingRepository.GetFullObjectTreeByGroupId(opleidingGuid);

        var res = new OpleidingValidator()
            .ValidateOpleiding(opleiding)
            .Where(x => !x.Success)
            .ToList();
        return Ok();
    }
}
