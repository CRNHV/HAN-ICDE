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

    public async Task<OpdrachtDto?> Bekijk(int opdrachtId)
    {
        var dbOpdracht = await _opdrachtRepository.GetById(opdrachtId);
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

        return dbOpdrachten.ConvertAll(x => new OpdrachtDto(x.Type == OpdrachtType.Toets)
        {
            Beschrijving = x.Beschrijving,
            GroupId = x.GroupId,
            Naam = x.Naam,

        });
    }

    public async Task<List<IngeleverdeOpdrachtDto>> HaalInzendingenOp(int opdrachtId)
    {
        List<IngeleverdeOpdracht> dbInzendingen = await _opdrachtRepository.HaalInzendingenOp(opdrachtId);
        return dbInzendingen.ConvertAll(x => new IngeleverdeOpdrachtDto()
        {
            Id = x.Id,
            Naam = x.Naam,
        });
    }

    public async Task LeverOpdrachtIn(int userId, LeverOpdrachtInDto opdracht)
    {
        var dbOpdracht = await _opdrachtRepository.GetById(opdracht.OpdrachtId);
        if (dbOpdracht is null)
            return;

        var bestandsLocatie = await _fileManager.SlaBestandOp(opdracht.Bestand.FileName, opdracht.Bestand);

        var ingeleverdeOpdracht = new IngeleverdeOpdracht()
        {
            Opdracht = dbOpdracht,
            OpdrachtId = dbOpdracht.Id,
            Naam = opdracht.Bestand.FileName,
            Locatie = bestandsLocatie,
        };

        await _opdrachtRepository.SlaIngeleverdeOpdrachtOp(ingeleverdeOpdracht);
    }

    public async Task MaakOpdracht(MaakOpdrachtDto opdracht)
    {
        await _opdrachtRepository.MaakOpdracht(opdracht.Naam, opdracht.Beschrijving, opdracht.IsToets);
    }

    public async Task SlaBeoordelingOp(OpdrachtBeoordelingDto request)
    {
        if (request.Beoordeling <= 0 || request.Beoordeling >= 11)
            return;

        var dbIngeleverdeOpdracht = await _opdrachtRepository.HaalInzendingOp(request.InzendingId);
        if (dbIngeleverdeOpdracht is null)
            return;

        await _opdrachtRepository.SlaBeoordelingOp(new OpdrachtBeoordeling()
        {
            Cijfer = request.Beoordeling,
            Feedback = request.Feedback,
            IngeleverdeOpdracht = dbIngeleverdeOpdracht
        });
    }
}
