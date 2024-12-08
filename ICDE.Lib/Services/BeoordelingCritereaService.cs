using AutoMapper;
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

    public async Task<List<BeoordelingCritereaDto>> GetAllUnique()
    {
        var beoordelingCritereas = await _beoordelingCritereaRepository.GetList();
        return _mapper.Map<List<BeoordelingCritereaDto>>(beoordelingCritereas);
    }
}
