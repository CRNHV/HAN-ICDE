using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Services.Base;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class BeoordelingCritereaService : VersionableServiceBase<BeoordelingCriterea, BeoordelingCritereaDto, MaakBeoordelingCritereaDto, UpdateBeoordelingCritereaDto>, IBeoordelingCritereaService
{
    private readonly IBeoordelingCritereaRepository _beoordelingCritereaRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateBeoordelingCritereaDto> _updateValidator;

    public BeoordelingCritereaService(IBeoordelingCritereaRepository beoordelingCritereaRepository,
                                      IMapper mapper,
                                      IValidator<MaakBeoordelingCritereaDto> createValidator,
                                      IValidator<UpdateBeoordelingCritereaDto> updateValidator) : base(beoordelingCritereaRepository, mapper, createValidator)
    {
        _beoordelingCritereaRepository = beoordelingCritereaRepository;
        _mapper = mapper;
        _updateValidator = updateValidator;
    }

    public async Task<BeoordelingCritereaMetEerdereVersiesDto?> HaalOpMetEerdereVersies(Guid critereaGroupId)
    {
        var dbCriterea = await _beoordelingCritereaRepository.NieuwsteVoorGroepId(critereaGroupId);
        if (dbCriterea is null)
        {
            return null;
        }
        var criterea = _mapper.Map<BeoordelingCritereaDto>(dbCriterea);

        var earlierVersions = await EerdereVersies(critereaGroupId, dbCriterea.VersieNummer);

        return new BeoordelingCritereaMetEerdereVersiesDto()
        {
            BeoordelingCriterea = criterea,
            EerdereVersies = _mapper.Map<List<BeoordelingCritereaDto>>(earlierVersions),
        };
    }

    public override async Task<Guid> MaakKopie(Guid groupId, int versieNummer)
    {
        var dbCriterea = await _beoordelingCritereaRepository.Versie(groupId, versieNummer);
        var critereaClone = (BeoordelingCriterea)dbCriterea.Clone();
        critereaClone.GroupId = Guid.NewGuid();
        await _beoordelingCritereaRepository.Maak(critereaClone);
        return critereaClone.GroupId;
    }

    public override async Task<bool> Update(UpdateBeoordelingCritereaDto request)
    {
        _updateValidator.ValidateAndThrow(request);

        var dbCriterea = await _beoordelingCritereaRepository.NieuwsteVoorGroepId(request.GroupId);
        if (dbCriterea is null)
            return false;

        dbCriterea.Naam = request.Naam;
        dbCriterea.Beschrijving = request.Beschrijving;

        var updateResult = await _beoordelingCritereaRepository.Update(dbCriterea);
        if (!updateResult)
            return false;

        var updatedCriterea = await _beoordelingCritereaRepository.NieuwsteVoorGroepId(request.GroupId);
        if (updatedCriterea is null)
            return false;

        if (dbCriterea.Leeruitkomsten.Count == 0)
            return true;

        updatedCriterea.Leeruitkomsten.AddRange(dbCriterea.Leeruitkomsten);
        updatedCriterea.RelationshipChanged = true;

        return await _beoordelingCritereaRepository.Update(updatedCriterea);
    }
}
