using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Vak;
using ICDE.Lib.Services.Base;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class VakService : VersionableServiceBase<Vak, VakDto, MaakVakDto, UpdateVakDto>, IVakService
{
    private readonly IVakRepository _vakRepository;
    private readonly ICursusRepository _cursusRepository;
    private readonly ILeeruitkomstRepository _leeruitkomstRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateVakDto> _updateValidator;

    public VakService(IVakRepository vakRepository,
        ICursusRepository cursusRepository,
        ILeeruitkomstRepository leeruitkomstRepository,
        IMapper mapper,
        IValidator<MaakVakDto> createValidator,
        IValidator<UpdateVakDto> updateValidator) : base(vakRepository, mapper, createValidator)
    {
        _vakRepository = vakRepository;
        _cursusRepository = cursusRepository;
        _leeruitkomstRepository = leeruitkomstRepository;
        _mapper = mapper;
        _updateValidator = updateValidator;
    }

    public async Task<VakMetOnderwijsOnderdelenDto?> HaalVolledigeVakDataOp(Guid vakGroupId)
    {
        var vak = await _vakRepository.NieuwsteVoorGroepId(vakGroupId);
        if (vak is null)
        {
            return null;
        }

        var result = _mapper.Map<VakMetOnderwijsOnderdelenDto>(vak);
        var eerdereVersies = await _vakRepository.Lijst(x => x.GroupId == vakGroupId && x.Id != vak.Id);
        result.EerdereVersies = _mapper.Map<List<VakDto>>(eerdereVersies);

        return result;
    }

    public async Task<bool> KoppelCursus(Guid vakGroupId, Guid cursusGroupId)
    {
        var dbVak = await _vakRepository.NieuwsteVoorGroepId(vakGroupId);
        if (dbVak is null)
        {
            return false;
        }
        var cursus = await _cursusRepository.NieuwsteVoorGroepId(cursusGroupId);
        if (cursus is null)
        {
            return false;
        }

        if (!dbVak.Cursussen.Contains(cursus))
        {
            dbVak.Cursussen.Add(cursus);
            dbVak.RelationshipChanged = true;

            await _vakRepository.Update(dbVak);
        }

        return true;
    }

    public async Task<bool> KoppelLeeruitkomst(Guid vakGroupId, Guid lukGroupId)
    {
        var dbVak = await _vakRepository.NieuwsteVoorGroepId(vakGroupId);
        if (dbVak is null)
        {
            return false;
        }
        var luk = await _leeruitkomstRepository.NieuwsteVoorGroepId(lukGroupId);
        if (luk is null)
        {
            return false;
        }
        if (!dbVak.Leeruitkomsten.Contains(luk))
        {
            dbVak.Leeruitkomsten.Add(luk);
            dbVak.RelationshipChanged = true;

            await _vakRepository.Update(dbVak);
        }

        return true;
    }

    public async Task<bool> OntkoppelCursus(Guid vakGroupId, Guid cursusGroupId)
    {
        var dbVak = await _vakRepository.NieuwsteVoorGroepId(vakGroupId);
        if (dbVak is null)
        {
            return false;
        }
        var cursus = await _cursusRepository.NieuwsteVoorGroepId(cursusGroupId);
        if (cursus is null)
        {
            return false;
        }

        if (!dbVak.Cursussen.Contains(cursus))
        {
            return true;
        }
        dbVak.Cursussen.Remove(cursus);
        dbVak.RelationshipChanged = true;

        await _vakRepository.Update(dbVak);

        return true;
    }

    public async Task<bool> OntkoppelLeeruitkomst(Guid vakGroupId, Guid lukGroupId)
    {
        var dbVak = await _vakRepository.NieuwsteVoorGroepId(vakGroupId);
        if (dbVak is null)
        {
            return false;
        }
        var luk = await _leeruitkomstRepository.NieuwsteVoorGroepId(lukGroupId);
        if (luk is null)
        {
            return false;
        }

        if (!dbVak.Leeruitkomsten.Contains(luk))
        {
            return true;
        }
        dbVak.Leeruitkomsten.Remove(luk);
        dbVak.RelationshipChanged = true;

        await _vakRepository.Update(dbVak);

        return true;
    }

    public override async Task<bool> Update(UpdateVakDto request)
    {
        _updateValidator.Validate(request);

        var vak = await _vakRepository.NieuwsteVoorGroepId(request.GroupId);
        if (vak is null)
        {
            return false;
        }

        vak.Naam = request.Naam;
        vak.Beschrijving = request.Beschrijving;
        await _vakRepository.Update(vak);

        var updatedVak = await _vakRepository.NieuwsteVoorGroepId(vak.GroupId);
        updatedVak.RelationshipChanged = true;
        updatedVak.Cursussen = vak.Cursussen;
        updatedVak.Leeruitkomsten = vak.Leeruitkomsten;
        await _vakRepository.Update(updatedVak);

        return true;
    }
}
