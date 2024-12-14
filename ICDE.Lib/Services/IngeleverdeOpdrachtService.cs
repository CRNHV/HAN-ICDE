using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Dto.OpdrachtInzending;
using ICDE.Lib.IO;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class IngeleverdeOpdrachtService : IIngeleverdeOpdrachtService
{
    private readonly IIngeleverdeOpdrachtRepository _ingeleverdeOpdrachtRepository;
    private readonly IOpdrachtBeoordelingRepository _opdrachtBeoordelingRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IOpdrachtRepository _opdrachtRepository;
    private readonly IFileManager _fileManager;
    private readonly IMapper _mapper;


    public IngeleverdeOpdrachtService(IIngeleverdeOpdrachtRepository ingeleverdeOpdrachtRepository, IOpdrachtRepository opdrachtRepository, IFileManager fileManager, IOpdrachtBeoordelingRepository opdrachtBeoordelingRepository, IMapper mapper, IStudentRepository studentRepository)
    {
        _ingeleverdeOpdrachtRepository = ingeleverdeOpdrachtRepository;
        _opdrachtRepository = opdrachtRepository;
        _fileManager = fileManager;
        _opdrachtBeoordelingRepository = opdrachtBeoordelingRepository;
        _mapper = mapper;
        _studentRepository = studentRepository;
    }

    public async Task<OpdrachtInzendingDto?> HaalInzendingDataOp(int inzendingId)
    {
        var inzending = await _ingeleverdeOpdrachtRepository.VoorId(inzendingId);
        if (inzending is null)
        {
            return null;
        }

        var opdracht = await _opdrachtRepository.GetByInzendingId(inzendingId);
        if (opdracht is null)
        {
            return null;
        }

        return new OpdrachtInzendingDto()
        {
            IngeleverdeOpdracht = _mapper.Map<IngeleverdeOpdrachtDto>(inzending),
            Beoordelingen = _mapper.Map<List<OpdrachtBeoordelingDto>>(inzending.Beoordelingen),
            BeoordelingCritereas = _mapper.Map<List<BeoordelingCritereaDto>>(opdracht.BeoordelingCritereas)
        };
    }

    public async Task<List<IngeleverdeOpdrachtDto>> HaalInzendingenOp(Guid opdrachtId)
    {
        List<IngeleverdeOpdracht> dbInzendingen = await _ingeleverdeOpdrachtRepository.Lijst(x => x.Opdracht.GroupId == opdrachtId);
        if (dbInzendingen.Count == 0)
        {
            return new List<IngeleverdeOpdrachtDto>();
        }
        return dbInzendingen.ConvertAll(x => new IngeleverdeOpdrachtDto()
        {
            Id = x.Id,
            Naam = x.Naam,
        });
    }

    public async Task<bool> LeverOpdrachtIn(int userId, LeverOpdrachtInDto opdracht)
    {
        var dbOpdracht = await _opdrachtRepository.VoorId(opdracht.OpdrachtId);
        if (dbOpdracht is null)
            return false;

        int? studentNummer = await _studentRepository.ZoekStudentNummerVoorUserId(userId);
        if (studentNummer is null)
        {
            return false;
        }

        var bestandsLocatie = await _fileManager.SlaBestandOp(opdracht.Bestand.FileName, opdracht.Bestand);
        if (string.IsNullOrEmpty(bestandsLocatie))
        {
            return false;
        }

        var ingeleverdeOpdracht = new IngeleverdeOpdracht()
        {
            Opdracht = dbOpdracht,
            OpdrachtId = dbOpdracht.Id,
            Naam = opdracht.Bestand.FileName,
            Locatie = bestandsLocatie,
            StudentNummer = studentNummer.Value,
        };

        await _ingeleverdeOpdrachtRepository.Maak(ingeleverdeOpdracht);
        return true;
    }

    public async Task<bool> SlaBeoordelingOp(OpdrachtBeoordelingDto request)
    {
        if (request.Cijfer <= 0 || request.Cijfer >= 11)
            return false;

        var dbIngeleverdeOpdracht = await _ingeleverdeOpdrachtRepository.VoorId(request.InzendingId);
        if (dbIngeleverdeOpdracht is null)
            return false;

        await _opdrachtBeoordelingRepository.Maak(new OpdrachtBeoordeling()
        {
            Cijfer = request.Cijfer,
            Feedback = request.Feedback,
            IngeleverdeOpdracht = dbIngeleverdeOpdracht
        });

        return true;
    }
}
