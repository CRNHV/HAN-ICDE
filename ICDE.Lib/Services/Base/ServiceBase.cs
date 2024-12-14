﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ICDE.Data.Entities.Base;
using ICDE.Data.Repositories.Base;

namespace ICDE.Lib.Services.Base;

public interface IVersionableServiceBase<TReadDto, TCreateDto, TUpdateDto> : ICrudServiceBase<TReadDto, TCreateDto, TUpdateDto>
{
    Task<List<TReadDto>> AlleUnieke();
    Task<TReadDto?> BekijkVersie(Guid groupId, int versieNummer);
    Task<List<TReadDto>> EerdereVersies(Guid groupId, int versieNummer);
    Task<Guid> MaakKopie(Guid groupId, int versieNummer);
    Task<bool> VerwijderVersie(Guid groupId, int versieNummer);
    Task<TReadDto?> NieuwsteVoorGroepId(Guid groupId);
}

public abstract class VersionableServiceBase<TEntity, TReadDto, TCreateDto, TUpdateDto> : CrudServiceBase<TEntity, TReadDto, TCreateDto, TUpdateDto>, IVersionableServiceBase<TReadDto, TCreateDto, TUpdateDto> where TEntity : class
{
    private readonly IVersionableRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    protected VersionableServiceBase(IVersionableRepository<TEntity> repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
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
        var createEntity = _mapper.Map<TEntity>(request);
        ((IVersionable)createEntity).GroupId = Guid.NewGuid();
        TEntity result = await _repository.Maak(createEntity);
        if (result is null)
        {
            return default;
        }

        return _mapper.Map<TReadDto?>(result);

    }
    public abstract Task<Guid> MaakKopie(Guid groupId, int versieNummer);

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

public interface ICrudServiceBase<TDto, TCreateDto, TUpdateDto>
{
    Task<TDto?> Maak(TCreateDto request);
    Task<bool> Update(TUpdateDto request);
    Task<bool> Verwijder(int id);
}

public abstract class CrudServiceBase<TEntity, TDto, TCreateDto, TUpdateDto> : ICrudServiceBase<TDto, TCreateDto> where TEntity : class
{
    private readonly ICrudRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    protected CrudServiceBase(ICrudRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TDto?> Maak(TCreateDto request)
    {
        var createEntity = _mapper.Map<TEntity>(request);
        TEntity result = await _repository.Maak(createEntity);
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