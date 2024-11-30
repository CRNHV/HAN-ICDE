using AutoMapper;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class CursusService : ICursusService
{
    private readonly ICursusRepository _cursusRepository;
    private readonly IMapper _mapper;

    public CursusService(ICursusRepository cursusRepository, IMapper mapper)
    {
        _cursusRepository = cursusRepository;
        _mapper = mapper;
    }

    public async Task<List<CursusDto>> GetAll()
    {
        var cursussen = await _cursusRepository.GetList();

        return _mapper.Map<List<CursusDto>>(cursussen);
    }

    public async Task<CursusMetPlanningDto> GetFullCursusByGroupId(Guid cursusGroupId)
    {
        var cursus = await _cursusRepository.GetFullCursusData(cursusGroupId);
        return _mapper.Map<CursusMetPlanningDto>(cursus);
    }

    public async Task<List<CursusDto>> GetEarlierVersionsByGroupId(Guid groupId, int exceptId)
    {
        var cursussen = await _cursusRepository.GetList(x => x.GroupId == groupId && x.Id != exceptId);
        return _mapper.Map<List<CursusDto>>(cursussen);
    }
}
