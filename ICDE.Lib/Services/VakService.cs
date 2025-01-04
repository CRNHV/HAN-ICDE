using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Data.Repositories.Luk;
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
        var vakken = await _vakRepository.Lijst(x => x.GroupId == vakGroupId);
        if (vakken.Count == 0)
        {
            return false;
        }
        var cursus = await _cursusRepository.NieuwsteVoorGroepId(cursusGroupId);
        if (cursus is null)
        {
            return false;
        }
        foreach (var vak in vakken)
        {
            if (!vak.Cursussen.Contains(cursus))
            {
                vak.Cursussen.Add(cursus);
                vak.RelationshipChanged = true;

                await _vakRepository.Update(vak);
            }
        }

        return true;
    }

    public async Task<bool> KoppelLeeruitkomst(Guid vakGroupId, Guid lukGroupId)
    {
        var vakken = await _vakRepository.Lijst(x => x.GroupId == vakGroupId);
        if (vakken.Count == 0)
        {
            return false;
        }
        var luk = await _leeruitkomstRepository.NieuwsteVoorGroepId(lukGroupId);
        if (luk is null)
        {
            return false;
        }
        foreach (var vak in vakken)
        {
            if (!vak.Leeruitkomsten.Contains(luk))
            {
                vak.Leeruitkomsten.Add(luk);
                vak.RelationshipChanged = true;

                await _vakRepository.Update(vak);
            }
        }

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
