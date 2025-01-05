using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Services.Base;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal sealed class OpdrachtService : VersionableServiceBase<Opdracht, OpdrachtDto, MaakOpdrachtDto, UpdateOpdrachtDto>, IOpdrachtService
{
    private readonly IOpdrachtRepository _opdrachtRepository;
    private readonly IBeoordelingCritereaRepository _beoordelingCritereaRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateOpdrachtDto> _updateValidator;


    public OpdrachtService(IOpdrachtRepository opdrachtRepository,
                           IMapper mapper,
                           IBeoordelingCritereaRepository beoordelingCritereaRepository,
                           IValidator<MaakOpdrachtDto> createValidator,
                           IValidator<UpdateOpdrachtDto> updateValidator) : base(opdrachtRepository, mapper, createValidator)
    {
        _opdrachtRepository = opdrachtRepository;
        _mapper = mapper;
        _beoordelingCritereaRepository = beoordelingCritereaRepository;
        _updateValidator = updateValidator;
    }

    public async Task<OpdrachtVolledigeDataDto?> HaalAlleDataOp(Guid opdrachtGroupId)
    {
        var opdracht = await _opdrachtRepository.GetFullDataByGroupId(opdrachtGroupId);
        if (opdracht is null)
        {
            return null;
        }

        var earlierVersions = await _opdrachtRepository.EerdereVersies(opdrachtGroupId, opdracht.Id);
        return new OpdrachtVolledigeDataDto()
        {
            BeoordelingCritereas = _mapper.Map<List<BeoordelingCritereaDto>>(opdracht.BeoordelingCritereas),
            EerdereVersies = _mapper.Map<List<OpdrachtDto>>(earlierVersions),
            Opdracht = _mapper.Map<OpdrachtDto>(opdracht),
        };
    }

    public async Task<bool> VoegCritereaToe(Guid opdrachtGroupId, Guid critereaGroupId)
    {
        var opdracht = await _opdrachtRepository.GetFullDataByGroupId(opdrachtGroupId);
        if (opdracht is null)
        {
            return false;
        }

        var criterea = await _beoordelingCritereaRepository.NieuwsteVoorGroepId(critereaGroupId);
        if (criterea is null)
        {
            return false;
        }

        if (opdracht.BeoordelingCritereas.Contains(criterea))
            return true;

        opdracht.BeoordelingCritereas.Add(criterea);
        opdracht.RelationshipChanged = true;
        return await _opdrachtRepository.Update(opdracht);
    }

    public async Task<StudentOpdrachtDto?> HaalStudentOpdrachtDataOp(Guid opdrachtGroupId)
    {
        var opdracht = await _opdrachtRepository.GetFullDataByGroupId(opdrachtGroupId);
        if (opdracht is null)
        {
            return null;
        }

        return new StudentOpdrachtDto()
        {
            OpdrachtId = opdracht.Id,
            Opdracht = _mapper.Map<OpdrachtDto>(opdracht),
            BeoordelingCritereas = _mapper.Map<List<BeoordelingCritereaDto>>(opdracht.BeoordelingCritereas)
        };
    }

    public override async Task<bool> Update(UpdateOpdrachtDto request)
    {
        _updateValidator.ValidateAndThrow(request);

        var opdracht = await _opdrachtRepository.GetFullDataByGroupId(request.GroupId);
        if (opdracht is null)
        {
            return false;
        }
        opdracht.Naam = request.Naam;
        opdracht.Beschrijving = request.Beschrijving;
        opdracht.Type = request.IsToets ? OpdrachtType.Toets : OpdrachtType.Casus;
        await _opdrachtRepository.Update(opdracht);

        var updatedOpdracht = await _opdrachtRepository.GetFullDataByGroupId(opdracht.GroupId);
        if (updatedOpdracht is null)
        {
            return false;
        }

        updatedOpdracht.BeoordelingCritereas.AddRange(opdracht.BeoordelingCritereas);
        updatedOpdracht.RelationshipChanged = true;
        return await _opdrachtRepository.Update(updatedOpdracht);
    }

    public async Task<bool> RemoveCriterea(Guid opdrachtGroupId, Guid critereaGroupId)
    {
        var opdracht = await _opdrachtRepository.GetFullDataByGroupId(opdrachtGroupId);
        if (opdracht is null)
        {
            return false;
        }

        var criterea = await _beoordelingCritereaRepository.NieuwsteVoorGroepId(critereaGroupId);
        if (criterea is null)
        {
            return false;
        }

        if (!opdracht.BeoordelingCritereas.Contains(criterea))
            return true;

        opdracht.BeoordelingCritereas.Remove(criterea);
        opdracht.RelationshipChanged = true;
        return await _opdrachtRepository.Update(opdracht);
    }
}
