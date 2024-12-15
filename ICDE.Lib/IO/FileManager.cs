using Microsoft.AspNetCore.Http;

namespace ICDE.Lib.IO;
internal sealed class FileManager : IFileManager
{
    private const string OpdrachtenMap = "./opdrachten";
    private const string RapportageMap = "./rapportage";

    public async Task<string> SlaOpdrachtOp(string naam, IFormFile bestand)
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

    public async Task<string> SlaRapportageOp(string naam, byte[] bytes)
    {
        try
        {
            if (!Directory.Exists(RapportageMap))
                Directory.CreateDirectory(RapportageMap);
            var fileDirectory = $"{RapportageMap}/{Guid.NewGuid()}/";
            Directory.CreateDirectory(fileDirectory);
            var filePath = Path.Combine(fileDirectory, naam);
            var fileStream = new FileStream(filePath, FileMode.Create);
            await fileStream.WriteAsync(bytes, 0, bytes.Length);
            await fileStream.FlushAsync();
            return filePath;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }
}
