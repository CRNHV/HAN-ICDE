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
        if (cursussen.Count == 0)
            return new List<CursusDto>();

        return _mapper.Map<List<CursusDto>>(cursussen);
    }

    public async Task<CursusMetPlanningDto?> GetFullCursusByGroupId(Guid cursusGroupId)
    {
        var cursus = await _cursusRepository.GetFullObjectTreeByGroupId(cursusGroupId);
        if (cursus is null)
            return null;

        return _mapper.Map<CursusMetPlanningDto>(cursus);
    }

    public async Task<List<CursusDto>> GetEarlierVersionsByGroupId(Guid groupId, int exceptId)
    {
        var cursussen = await _cursusRepository.GetList(x => x.GroupId == groupId && x.Id != exceptId);
        if (cursussen.Count == 0)
            return new List<CursusDto>();
        return _mapper.Map<List<CursusDto>>(cursussen);
    }

    public async Task<bool> VoegPlanningToeAanCursus(Guid cursusGroupId, int planningId)
    {
        var cursus = await _cursusRepository.GetLatestByGroupId(cursusGroupId);
        if (cursus is null)
            return false;

        var planning = await _planningRepository.CreateCloneOf(planningId);
        if (planning is null)
            return false;

        cursus.RelationshipChanged = true;
        cursus.Planning = planning;

        var result = await _cursusRepository.Update(cursus);
        return result != null;
    }
}
