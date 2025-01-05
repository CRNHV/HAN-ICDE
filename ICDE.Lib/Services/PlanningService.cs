using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Services.Base;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class PlanningService : CrudServiceBase<Planning, PlanningDto, MaakPlanningDto, UpdatePlanningDto>, IPlanningService
{
    private readonly ICursusRepository _cursusRepository;
    private readonly IPlanningRepository _planningRepository;
    private readonly IOpdrachtRepository _opdrachtRepository;
    private readonly ILesRepository _lesRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdatePlanningDto> _updateValidator;

    public PlanningService(IPlanningRepository planningRepository,
                           IMapper mapper,
                           ICursusRepository cursusRepository,
                           IOpdrachtRepository opdrachtRepository,
                           ILesRepository lesRepository,
                           IValidator<MaakPlanningDto> createValidator,
                           IValidator<UpdatePlanningDto> updateValidator) : base(planningRepository, mapper, createValidator)
    {
        _planningRepository = planningRepository;
        _mapper = mapper;
        _opdrachtRepository = opdrachtRepository;
        _lesRepository = lesRepository;
        _updateValidator = updateValidator;
        _cursusRepository = cursusRepository;
    }

    public async Task<PlanningZonderItemsDto?> VoegOpdrachtToe(int planningId, Guid groupId)
    {
        var planning = await _planningRepository.VoorId(planningId);
        if (planning is null)
        {
            return null;
        }
        var opdracht = await _opdrachtRepository.NieuwsteVoorGroepId(groupId);
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
        if (result == false)
        {
            return null;
        }

        return _mapper.Map<PlanningZonderItemsDto>(planning);
    }

    public async Task<PlanningZonderItemsDto?> VoegLesToe(int planningId, Guid groupId)
    {
        var planning = await _planningRepository.VoorId(planningId);
        if (planning is null)
        {
            return null;
        }

        var les = await _lesRepository.NieuwsteVoorGroepId(groupId);
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
        if (result == false)
        {
            return null;
        }
        return _mapper.Map<PlanningZonderItemsDto>(planning);
    }

    public async Task<List<LesMetLeeruitkomstenDto>> HaalLessenOpVoorPlanning(int planningId)
    {
        var planning = await _planningRepository.VoorId(planningId);
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

    public override async Task<bool> Update(UpdatePlanningDto request)
    {
        _updateValidator.ValidateAndThrow(request);

        var planningToUpdate = await _planningRepository.VoorId(request.Id);
        if (planningToUpdate is null)
        {
            return false;
        }

        planningToUpdate.Name = request.Name;

        await _planningRepository.Update(planningToUpdate);
        return true;
    }

    public async Task<List<PlanningZonderItemsDto>> AlleUnieke()
    {
        var plannings = await _planningRepository.Lijst();
        if (plannings.Count == 0)
        {
            return new List<PlanningZonderItemsDto>();
        }
        return _mapper.Map<List<PlanningZonderItemsDto>>(plannings);
    }

    public async Task<PlanningDto?> VoorId(int planningId)
    {
        var planning = await _planningRepository.VoorId(planningId);
        if (planning is null)
        {
            return null;
        }
        return _mapper.Map<PlanningDto>(planning);
    }

    public async Task<bool> VerwijderPlanning(int planningid)
    {
        var cursussen = await _cursusRepository.GetCursussenWithPlanningByPlanningId(planningid);

        foreach (var cursus in cursussen)
        {
            cursus.RelationshipChanged = true;
            cursus.Planning = null;
            await _cursusRepository.Update(cursus);
        }

        var planning = await _planningRepository.VoorId(planningid);
        if (planning == null)
        {
            return false;
        }

        return await _planningRepository.Verwijder(planning);
    }
}
