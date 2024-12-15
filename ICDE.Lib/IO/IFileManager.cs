using Microsoft.AspNetCore.Http;

namespace ICDE.Lib.IO;
internal interface IFileManager
{
    Task<string> SlaOpdrachtOp(string naam, IFormFile bestand);
    Task<string> SlaRapportageOp(string naam, byte[] bytes);
}
