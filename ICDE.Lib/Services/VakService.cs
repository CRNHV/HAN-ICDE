using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Data.Repositories.Luk;
using ICDE.Lib.Dto.Vak;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class VakService : IVakService
{
    private readonly IVakRepository _vakRepository;
    private readonly ICursusRepository _cursusRepository;
    private readonly ILeeruitkomstRepository _leeruitkomstRepository;
    private readonly IMapper _mapper;

    public VakService(IVakRepository vakRepository,
        ICursusRepository cursusRepository,
        ILeeruitkomstRepository leeruitkomstRepository,
        IMapper mapper)
    {
        _vakRepository = vakRepository;
        _cursusRepository = cursusRepository;
        _leeruitkomstRepository = leeruitkomstRepository;
        _mapper = mapper;
    }

    public async Task<Guid> MaakVak(string naam, string beschrijving)
    {
        var course = await _vakRepository.Create(new Vak()
        {
            Naam = naam,
            Beschrijving = beschrijving,
            GroupId = Guid.NewGuid(),
        });

        if (course is null)
        {
            return Guid.Empty;
        }

        return course.GroupId;
    }

    public async Task<bool> VerwijderVersie(Guid vakGroupId, int vakVersie)
    {
        var vakken = await _vakRepository.GetList(x => x.GroupId == vakGroupId && x.VersieNummer == vakVersie);
        foreach (var item in vakken)
        {
            await _vakRepository.Delete(item);
        }

        return true;
    }

    public async Task<List<VakDto>> Allemaal()
    {
        var vakken = await _vakRepository.GetList();
        if (vakken.Count == 0)
        {
            return new List<VakDto>();
        }
        return _mapper.Map<List<VakDto>>(vakken);
    }

    public async Task<VakMetOnderwijsOnderdelenDto?> HaalVolledigeVakDataOp(Guid vakGroupId)
    {
        var vak = await _vakRepository.GetLatestByGroupId(vakGroupId);
        if (vak is null)
        {
            return null;
        }

        var result = _mapper.Map<VakMetOnderwijsOnderdelenDto>(vak);
        var eerdereVersies = await _vakRepository.GetList(x => x.GroupId == vakGroupId && x.Id != vak.Id);
        result.EerdereVersies = _mapper.Map<List<VakDto>>(eerdereVersies);

        return result;
    }

    public async Task<bool> KoppelCursus(Guid vakGroupId, Guid cursusGroupId)
    {
        var vakken = await _vakRepository.GetList(x => x.GroupId == vakGroupId);
        if (vakken.Count == 0)
        {
            return false;
        }
        var cursus = await _cursusRepository.GetLatestByGroupId(cursusGroupId);
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
        var vakken = await _vakRepository.GetList(x => x.GroupId == vakGroupId);
        if (vakken.Count == 0)
        {
            return false;
        }
        var luk = await _leeruitkomstRepository.GetLatestByGroupId(lukGroupId);
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

    public async Task<bool> Update(UpdateVakDto request)
    {
        var vak = await _vakRepository.GetLatestByGroupId(request.GroupId);
        if (vak is null)
        {
            return false;
        }

        vak.Naam = request.Naam;
        vak.Beschrijving = request.Beschrijving;
        await _vakRepository.Update(vak);

        var updatedVak = await _vakRepository.GetLatestByGroupId(vak.GroupId);
        updatedVak.RelationshipChanged = true;
        updatedVak.Cursussen = vak.Cursussen;
        updatedVak.Leeruitkomsten = vak.Leeruitkomsten;
        await _vakRepository.Update(updatedVak);

        return true;
    }
}
