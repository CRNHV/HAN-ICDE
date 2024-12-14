using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Services.Base;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class CursusService : VersionableServiceBase<Cursus, CursusDto, MaakCursusDto, UpdateCursusDto>, ICursusService
{
    private readonly ICursusRepository _cursusRepository;
    private readonly IPlanningRepository _planningRepository;
    private readonly IMapper _mapper;

    public CursusService(ICursusRepository cursusRepository, IMapper mapper, IPlanningRepository planningRepository) : base(cursusRepository, mapper)
    {
        _cursusRepository = cursusRepository;
        _mapper = mapper;
        _planningRepository = planningRepository;
    }


    public async Task<CursusMetPlanningDto?> HaalAlleDataOp(Guid cursusGroupId)
    {
        var cursus = await _cursusRepository.GetFullObjectTreeByGroupId(cursusGroupId);
        if (cursus is null)
            return null;

        return _mapper.Map<CursusMetPlanningDto>(cursus);
    }

    public async Task<bool> VoegPlanningToeAanCursus(Guid cursusGroupId, int planningId)
    {
        var cursus = await _cursusRepository.NieuwsteVoorGroepId(cursusGroupId);
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

    public override Task<Guid> MaakKopie(Guid groupId, int versieNummer)
    {
        throw new NotImplementedException();
    }

    public override Task<bool> Update(UpdateCursusDto request)
    {
        throw new NotImplementedException();
    }
}
