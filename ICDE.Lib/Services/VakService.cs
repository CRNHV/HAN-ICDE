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

    public async Task<Guid> CreateCourse(string naam, string beschrijving)
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

    public async Task<List<VakDto>> GetAll()
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

        return _mapper.Map<VakMetOnderwijsOnderdelenDto>(vak);
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
}
