using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Lessen;
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
        if (plannings.Count == 0)
        {
            return new List<PlanningZonderItemsDto>();
        }
        return _mapper.Map<List<PlanningZonderItemsDto>>(plannings);
    }

    public async Task<PlanningDto?> GetById(int planningId)
    {
        var planning = await _planningRepository.Get(planningId);
        if (planning is null)
        {
            return null;
        }
        return _mapper.Map<PlanningDto>(planning);
    }

    public async Task<PlanningZonderItemsDto?> VoegOpdrachtToe(int planningId, Guid groupId)
    {
        var planning = await _planningRepository.Get(planningId);
        if (planning is null)
        {
            return null;
        }
        var opdracht = await _opdrachtRepository.GetLatestByGroupId(groupId);
        if (opdracht is null)
        {
            return null;
        }

        var nextItemIndex = planning.PlanningItems.OrderByDescending(x => x.Index).First().Index;
        nextItemIndex++;

        planning.PlanningItems.Add(new PlanningItem()
        {
            Index = nextItemIndex,
            Opdracht = opdracht,
            OpdrachtId = opdracht.Id,
        });

        var result = await _planningRepository.Update(planning);
        if (result is null)
        {
            return null;
        }

        return _mapper.Map<PlanningZonderItemsDto>(planning);
    }

    public async Task<PlanningZonderItemsDto?> VoegLesToe(int planningId, Guid groupId)
    {
        var planning = await _planningRepository.Get(planningId);
        if (planning is null)
        {
            return null;
        }

        var les = await _lesRepository.GetLatestByGroupId(groupId);
        if (les is null)
        {
            return null;
        }

        var nextItemIndex = planning.PlanningItems.OrderByDescending(x => x.Index).First().Index;
        nextItemIndex++;

        planning.PlanningItems.Add(new PlanningItem()
        {
            Index = nextItemIndex,
            Les = les,
            LesId = les.Id,
        });

        var result = await _planningRepository.Update(planning);
        if (result is null)
        {
            return null;
        }
        return _mapper.Map<PlanningZonderItemsDto>(planning);
    }

    public async Task<List<LesMetLeeruitkomstenDto>> GetLessonsForPlanning(int planningId)
    {
        var planning = await _planningRepository.Get(planningId);
        if (planning is null)
        {
            return new List<LesMetLeeruitkomstenDto>();
        }

        var lessons = planning.PlanningItems
            .Where(x => x.Les != null)
            .Select(x => x.Les)
            .ToList();
        return _mapper.Map<List<LesMetLeeruitkomstenDto>>(lessons);
    }
}
