using AutoMapper;
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

    public BeoordelingCritereaService(IBeoordelingCritereaRepository beoordelingCritereaRepository, IMapper mapper) : base(beoordelingCritereaRepository, mapper)
    {
        _beoordelingCritereaRepository = beoordelingCritereaRepository;
        _mapper = mapper;
    }

    public async Task<BeoordelingCritereaMetEerdereVersiesDto?> HaalOpMetEerdereVersies(Guid critereaGroupId)
    {
        var dbCriterea = await _beoordelingCritereaRepository.NieuwsteVoorGroepId(critereaGroupId);
        if (dbCriterea is null)
        {
            return null;
        }
        var criterea = _mapper.Map<BeoordelingCritereaDto>(dbCriterea);

        var earlierVersions = EerdereVersies(critereaGroupId, dbCriterea.VersieNummer);

        return new BeoordelingCritereaMetEerdereVersiesDto()
        {
            BeoordelingCriterea = criterea,
            EerdereVersies = _mapper.Map<List<BeoordelingCritereaDto>>(earlierVersions),
        };
    }

    public override Task<Guid> MaakKopie(Guid groupId, int versieNummer)
    {
        throw new NotImplementedException();
    }

    public override Task<bool> Update(UpdateBeoordelingCritereaDto request)
    {
        throw new NotImplementedException();
    }
}
