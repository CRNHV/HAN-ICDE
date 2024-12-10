using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.IO;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal sealed class OpdrachtService : IOpdrachtService
{
    private readonly IOpdrachtRepository _opdrachtRepository;
    private readonly IFileManager _fileManager;
    private readonly IBeoordelingCritereaRepository _beoordelingCritereaRepository;
    private readonly IMapper _mapper;

    public OpdrachtService(IOpdrachtRepository opdrachtRepository, IFileManager fileManager, IMapper mapper, IBeoordelingCritereaRepository beoordelingCritereaRepository)
    {
        _opdrachtRepository = opdrachtRepository;
        _fileManager = fileManager;
        _mapper = mapper;
        _beoordelingCritereaRepository = beoordelingCritereaRepository;
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

    public async Task<OpdrachtVolledigeDataDto?> HaalAlleDataOp(Guid opdrachtGroupId)
    {
        var opdracht = await _opdrachtRepository.GetFullDataByGroupId(opdrachtGroupId);
        if (opdracht is null)
        {
            return null;
        }

        var earlierVersions = await _opdrachtRepository.GetEarlierVersions(opdrachtGroupId, opdracht.Id);
        return new OpdrachtVolledigeDataDto()
        {
            BeoordelingCritereas = _mapper.Map<List<BeoordelingCritereaDto>>(opdracht.BeoordelingCritereas),
            EerdereVersies = _mapper.Map<List<OpdrachtDto>>(earlierVersions),
            Opdracht = _mapper.Map<OpdrachtDto>(opdracht),
        };
    }

    public async Task<List<OpdrachtDto>> Allemaal()
    {
        var dbOpdrachten = await _opdrachtRepository.GetList();
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

    public async Task MaakOpdracht(MaakOpdrachtDto request)
    {
        var opdracht = _mapper.Map<Opdracht>(request);
        opdracht.GroupId = Guid.NewGuid();
        await _opdrachtRepository.Create(opdracht);
    }

    public async Task VerwijderOpdracht(Guid opdrachtGroupId)
    {
        await _opdrachtRepository.Delete(opdrachtGroupId);
    }

    public async Task UpdateOpdracht(OpdrachtUpdateDto request)
    {
        var opdracht = await _opdrachtRepository.GetFullDataByGroupId(request.GroupId);
        opdracht.Naam = request.Naam;
        opdracht.Beschrijving = request.Beschrijving;
        opdracht.Type = request.IsToets ? OpdrachtType.Toets : OpdrachtType.Casus;
        await _opdrachtRepository.Update(opdracht);

        var updatedOpdracht = await _opdrachtRepository.GetFullDataByGroupId(opdracht.GroupId);

        foreach (var item in opdracht.BeoordelingCritereas)
        {
            updatedOpdracht.BeoordelingCritereas.Add(item);
        }
        updatedOpdracht.RelationshipChanged = true;
        await _opdrachtRepository.Update(updatedOpdracht);
    }

    public async Task<bool> VoegCritereaToe(Guid opdrachtGroupId, Guid critereaGroupId)
    {
        var opdrachten = await _opdrachtRepository.GetList(x => x.GroupId == opdrachtGroupId);
        if (opdrachten.Count == 0)
        {
            return false;
        }

        var criterea = await _beoordelingCritereaRepository.GetList(x => x.GroupId == critereaGroupId);
        if (criterea.Count == 0)
        {
            return false;
        }

        foreach (var item in opdrachten)
        {
            item.BeoordelingCritereas.Add(criterea.First());
            item.RelationshipChanged = true;
            await _opdrachtRepository.Update(item);
        }

        return true;
    }
}
