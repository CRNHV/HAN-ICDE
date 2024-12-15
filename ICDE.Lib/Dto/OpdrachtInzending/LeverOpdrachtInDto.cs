using Microsoft.AspNetCore.Http;

namespace ICDE.Lib.Dto.OpdrachtInzending;

public class LeverOpdrachtInDto
{
    public int OpdrachtId { get; set; }
    public string Naam { get; set; }
    public IFormFile Bestand { get; set; }
}
