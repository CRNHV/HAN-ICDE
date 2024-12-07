using AutoMapper;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class CursusService : ICursusService
{
    private readonly ICursusRepository _cursusRepository;
    private readonly IPlanningRepository _planningRepository;
    private readonly IMapper _mapper;

    public CursusService(ICursusRepository cursusRepository, IMapper mapper, IPlanningRepository planningRepository)
    {
        _cursusRepository = cursusRepository;
        _mapper = mapper;
        _planningRepository = planningRepository;
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

    public async Task VoegPlanningToeAanCursus(Guid cursusGroupId, int planningId)
    {
        var cursus = await _cursusRepository.GetLatestByGroupId(cursusGroupId);
        var planning = await _planningRepository.CreateCloneOf(planningId);

        cursus.RelationshipChanged = true;
        cursus.Planning = planning;

        await _cursusRepository.Update(cursus);
    }
}
