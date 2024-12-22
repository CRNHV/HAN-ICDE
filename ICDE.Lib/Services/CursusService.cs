using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Services.Base;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class CursusService : VersionableServiceBase<Cursus, CursusDto, MaakCursusDto, UpdateCursusDto>, ICursusService
{
    private readonly ICursusRepository _cursusRepository;
    private readonly IPlanningRepository _planningRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateCursusDto> _updateValidator;

    public CursusService(ICursusRepository cursusRepository,
                         IMapper mapper,
                         IPlanningRepository planningRepository,
                         IValidator<UpdateCursusDto> updateValidator,
                         IValidator<MaakCursusDto> createValidator) : base(cursusRepository, mapper, createValidator)
    {
        _cursusRepository = cursusRepository;
        _mapper = mapper;
        _planningRepository = planningRepository;
        _updateValidator = updateValidator;
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

    public override async Task<Guid> MaakKopie(Guid groupId, int versieNummer)
    {
        var cursus = await _cursusRepository.Versie(groupId, versieNummer);
        if (cursus is null)
        {
            return Guid.Empty;
        }
        var cursusClone = (Cursus)cursus.Clone();
        cursusClone.GroupId = Guid.NewGuid();
        await _cursusRepository.Maak(cursusClone);
        return cursusClone.GroupId;
    }

    public override async Task<bool> Update(UpdateCursusDto request)
    {
        _updateValidator.ValidateAndThrow(request);

        var dbCursus = await _cursusRepository.GetFullObjectTreeByGroupId(request.GroupId);
        if (dbCursus is null)
            return false;

        dbCursus.Naam = request.Naam;
        dbCursus.Beschrijving = request.Beschrijving;

        var updateResult = await _cursusRepository.Update(dbCursus);
        if (!updateResult)
            return false;

        var updatedCursus = await _cursusRepository.GetFullObjectTreeByGroupId(request.GroupId);
        if (updatedCursus is null)
            return false;

        if (dbCursus.Planning is null)
            return true;

        updatedCursus.Planning = (Planning)dbCursus.Planning.Clone();
        updatedCursus.RelationshipChanged = true;

        return await _cursusRepository.Update(updatedCursus);
    }
}
