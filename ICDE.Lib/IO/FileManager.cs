using Microsoft.AspNetCore.Http;

namespace ICDE.Lib.IO;
internal sealed class FileManager : IFileManager
{
    private const string OpdrachtenMap = "./opdrachten";

    public async Task<string> SlaBestandOp(string naam, IFormFile bestand)
    {
        try
        {
            if (!Directory.Exists(OpdrachtenMap))
                Directory.CreateDirectory(OpdrachtenMap);

            var fileDirectory = $"{OpdrachtenMap}/{Guid.NewGuid()}/";
            Directory.CreateDirectory(fileDirectory);

            var filePath = Path.Combine(fileDirectory, naam);

            var fileStream = new FileStream(filePath, FileMode.Create);
            await bestand.CopyToAsync(fileStream);

            await fileStream.FlushAsync();

            return filePath;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}
