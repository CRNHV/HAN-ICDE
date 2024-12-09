﻿using AutoMapper;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class BeoordelingCritereaService : IBeoordelingCritereaService
{
    private readonly IBeoordelingCritereaRepository _beoordelingCritereaRepository;
    private readonly IMapper _mapper;

    public BeoordelingCritereaService(IBeoordelingCritereaRepository beoordelingCritereaRepository, IMapper mapper)
    {
        _beoordelingCritereaRepository = beoordelingCritereaRepository;
        _mapper = mapper;
    }

    public async Task<List<BeoordelingCritereaDto>> Unieke()
    {
        var beoordelingCritereas = await _beoordelingCritereaRepository.GetList();
        return _mapper.Map<List<BeoordelingCritereaDto>>(beoordelingCritereas);
    }

    public async Task<BeoordelingCritereaMetEerdereVersiesDto?> HaalOpMetEerdereVersies(Guid critereaGroupId)
    {
        var dbCriterea = await _beoordelingCritereaRepository.GetFullDataByGroupId(critereaGroupId);
        if (dbCriterea is null)
        {
            return null;
        }
        var criterea = _mapper.Map<BeoordelingCritereaDto>(dbCriterea);

        var earlierVersions = await _beoordelingCritereaRepository.GetList(x => x.GroupId == dbCriterea.GroupId && x.Id != dbCriterea.Id);

        return new BeoordelingCritereaMetEerdereVersiesDto()
        {
            BeoordelingCriterea = criterea,
            EerdereVersies = _mapper.Map<List<BeoordelingCritereaDto>>(earlierVersions),
        };
    }
}
