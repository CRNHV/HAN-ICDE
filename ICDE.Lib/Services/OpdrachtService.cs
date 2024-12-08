using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.IO;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal sealed class OpdrachtService : IOpdrachtService
{
    private readonly IOpdrachtRepository _opdrachtRepository;
    private readonly IFileManager _fileManager;

    public OpdrachtService(IOpdrachtRepository opdrachtRepository, IFileManager fileManager)
    {
        _opdrachtRepository = opdrachtRepository;
        _fileManager = fileManager;
    }

    public async Task<OpdrachtDto?> Bekijk(Guid opdrachtId)
    {
        var dbOpdracht = await _opdrachtRepository.GetLatestByGroupId(opdrachtId);
        if (dbOpdracht is null)
            return null;

        return new OpdrachtDto(dbOpdracht.Type == OpdrachtType.Toets)
        {
            Beschrijving = dbOpdracht.Beschrijving,
            GroupId = dbOpdracht.GroupId,
            Naam = dbOpdracht.Naam,
        };
    }

    public async Task<List<OpdrachtDto>> GetAll()
    {
        var dbOpdrachten = await _opdrachtRepository.HaalAlleOp();
        if (dbOpdrachten.Count == 0)
        {
            return new List<OpdrachtDto>();
        }

        return dbOpdrachten.ConvertAll(x => new OpdrachtDto(x.Type == OpdrachtType.Toets)
        {
            Beschrijving = x.Beschrijving,
            GroupId = x.GroupId,
            Naam = x.Naam,

        });
    }

    public async Task MaakOpdracht(MaakOpdrachtDto opdracht)
    {
        await _opdrachtRepository.MaakOpdracht(opdracht.Naam, opdracht.Beschrijving, opdracht.IsToets);
    }
}
