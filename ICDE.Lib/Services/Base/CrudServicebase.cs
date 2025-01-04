using AutoMapper;
using FluentValidation;
using ICDE.Data.Repositories.Base;

namespace ICDE.Lib.Services.Base;

public abstract class CrudServiceBase<TEntity, TDto, TCreateDto, TUpdateDto> : ICrudServiceBase<TDto, TCreateDto, TUpdateDto> where TEntity : class
{
    private readonly ICrudRepository<TEntity> _repository;
    private readonly IValidator<TCreateDto> _createValidator;
    private readonly IMapper _mapper;

    protected CrudServiceBase(ICrudRepository<TEntity> repository, IMapper mapper, IValidator<TCreateDto> createValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
    }

    public async Task<TDto?> Maak(TCreateDto request)
    {
        _createValidator.ValidateAndThrow(request);

        var createEntity = _mapper.Map<TEntity>(request);
        if (createEntity is null)
        {
            return default;
        }

        TEntity? result = await _repository.Maak(createEntity);
        if (result is null)
        {
            return default;
        }

        return _mapper.Map<TDto?>(result);

    }

    public async Task<bool> Verwijder(int id)
    {
        TEntity? dbEntity = await _repository.VoorId(id);
        if (dbEntity is null)
        {
            return false;
        }

        await _repository.Verwijder(dbEntity);
        return true;
    }

    public abstract Task<bool> Update(TUpdateDto request);
}