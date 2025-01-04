using AutoMapper;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class OpdrachtBeoordelingService : IOpdrachtBeoordelingService
{
    private readonly IOpdrachtBeoordelingRepository _beoordelingRepository;
    private readonly IMapper _mapper;

    public OpdrachtBeoordelingService(IOpdrachtBeoordelingRepository beoordelingRepository, IMapper mapper)
    {
        _beoordelingRepository = beoordelingRepository;
        _mapper = mapper;
    }

    public async Task<List<OpdrachtMetBeoordelingDto>> HaalBeoordelingenOpVoorUser(int? userId)
    {
        if (userId is null)
        {
            return new List<OpdrachtMetBeoordelingDto>();
        }

        var beoordelingen = await _beoordelingRepository.HaalBeoordelingenOpVoorStudent(userId.Value);
        return _mapper.Map<List<OpdrachtMetBeoordelingDto>>(beoordelingen);
    }
}
