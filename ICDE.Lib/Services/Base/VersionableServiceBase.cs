using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities.Base;
using ICDE.Data.Repositories.Base;

namespace ICDE.Lib.Services.Base;
public abstract class VersionableServiceBase<TEntity, TReadDto, TCreateDto, TUpdateDto> : CrudServiceBase<TEntity, TReadDto, TCreateDto, TUpdateDto>, IVersionableServiceBase<TReadDto, TCreateDto, TUpdateDto> where TEntity : class
{
    private readonly IVersionableRepository<TEntity> _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<TCreateDto> _createValidator;

    protected VersionableServiceBase(IVersionableRepository<TEntity> repository, IMapper mapper, IValidator<TCreateDto> createValidator) : base(repository, mapper, createValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
    }

    public async Task<List<TReadDto>> AlleUnieke()
    {
        var dbEntities = await _repository.AlleUnieke();
        return _mapper.Map<List<TReadDto>>(dbEntities);
    }

    public async Task<List<TReadDto>> EerdereVersies(Guid groupId, int versieNummer)
    {
        var dbEerdereVersies = await _repository.EerdereVersies(groupId, versieNummer);
        return _mapper.Map<List<TReadDto>>(dbEerdereVersies);
    }

    public async Task<bool> VerwijderVersie(Guid groupId, int versieNummer)
    {
        TEntity? dbEntity = await _repository.Versie(groupId, versieNummer);
        if (dbEntity is null)
        {
            return false;
        }

        await _repository.Verwijder(dbEntity);
        return true;
    }
    public async Task<TReadDto?> BekijkVersie(Guid groupId, int versieNummer)
    {
        TEntity? dbVersie = await _repository.Versie(groupId, versieNummer);
        if (dbVersie is null)
        {
            return default;
        }

        return _mapper.Map<TReadDto>(dbVersie);
    }

    public new async Task<TReadDto?> Maak(TCreateDto request)
    {
        _createValidator.ValidateAndThrow(request);

        var createEntity = _mapper.Map<TEntity>(request);
        if (createEntity is null)
        {
            return default;
        }

        ((IVersionable)createEntity).GroupId = Guid.NewGuid();
        TEntity? result = await _repository.Maak(createEntity);
        if (result is null)
        {
            return default;
        }

        return _mapper.Map<TReadDto?>(result);

    }

    public async Task<Guid> MaakKopie(Guid groupId, int versieNummer)
    {
        var dbEntity = await _repository.Versie(groupId, versieNummer);
        if (dbEntity is null)
        {
            return Guid.Empty;
        }

        var clonedEntity = ((ICloneable)dbEntity).Clone();
        if (clonedEntity is null)
        {
            return Guid.Empty;
        }

        ((IVersionable)clonedEntity).GroupId = Guid.NewGuid();

        var result = await _repository.Maak((TEntity)clonedEntity);
        if (result is null)
        {
            return Guid.Empty;
        }

        // We're in a _versionable_ repository so we can cast it knowing it's IVersionable. 
        var resultGuid = ((IVersionable)result).GroupId;
        return resultGuid;
    }

    public async Task<TReadDto?> NieuwsteVoorGroepId(Guid groupId)
    {
        var dbEntity = await _repository.NieuwsteVoorGroepId(groupId);
        if (dbEntity is null)
        {
            return default;
        }

        return _mapper.Map<TReadDto>(dbEntity);
    }
}
