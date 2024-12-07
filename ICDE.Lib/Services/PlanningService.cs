using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class PlanningService : IPlanningService
{
    private readonly IPlanningRepository _planningRepository;
    private readonly IOpdrachtRepository _opdrachtRepository;
    private readonly ILesRepository _lesRepository;
    private readonly IMapper _mapper;

    public PlanningService(IPlanningRepository planningRepository, IMapper mapper, IOpdrachtRepository opdrachtRepository, ILesRepository lesRepository)
    {
        _planningRepository = planningRepository;
        _mapper = mapper;
        _opdrachtRepository = opdrachtRepository;
        _lesRepository = lesRepository;
    }

    public async Task<List<PlanningZonderItemsDto>> GetAll()
    {
        var plannings = await _planningRepository.GetList();
        return _mapper.Map<List<PlanningZonderItemsDto>>(plannings);
    }

    public async Task<PlanningDto> GetById(int planningId)
    {
        var planning = await _planningRepository.Get(planningId);
        return _mapper.Map<PlanningDto>(planning);
    }

    public async Task<PlanningZonderItemsDto> VoegOpdrachtToe(int planningId, Guid groupId)
    {
        var planning = await _planningRepository.Get(planningId);
        var opdracht = await _opdrachtRepository.GetLatestByGroupId(groupId);

        var nextItemIndex = planning.PlanningItems.OrderByDescending(x => x.Index).First().Index;
        nextItemIndex++;

        planning.PlanningItems.Add(new PlanningItem()
        {
            Index = nextItemIndex,
            Opdracht = opdracht,
            OpdrachtId = opdracht.Id,
        });

        await _planningRepository.Update(planning);

        return _mapper.Map<PlanningZonderItemsDto>(planning);
    }

    public async Task<PlanningZonderItemsDto> VoegLesToe(int planningId, Guid groupId)
    {
        var planning = await _planningRepository.Get(planningId);
        var les = await _lesRepository.GetLatestByGroupId(groupId);

        var nextItemIndex = planning.PlanningItems.OrderByDescending(x => x.Index).First().Index;
        nextItemIndex++;

        planning.PlanningItems.Add(new PlanningItem()
        {
            Index = nextItemIndex,
            Les = les,
            LesId = les.Id,
        });

        await _planningRepository.Update(planning);
        return _mapper.Map<PlanningZonderItemsDto>(planning);
    }
}
