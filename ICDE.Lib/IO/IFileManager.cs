using Microsoft.AspNetCore.Http;

namespace ICDE.Lib.IO;
internal interface IFileManager
{
    public Task<string> SlaBestandOp(string naam, IFormFile bestand);
}
