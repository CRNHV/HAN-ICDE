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

        return course.GroupId;
    }

    public async Task<List<VakDto>> GetAll()
    {
        var vakken = await _vakRepository.GetList();
        return _mapper.Map<List<VakDto>>(vakken);
    }

    public async Task<VakMetOnderwijsOnderdelenDto> HaalVolledigeVakDataOp(Guid vakGroupId)
    {
        var vak = await _vakRepository.GetLatestByGroupId(vakGroupId);

        return _mapper.Map<VakMetOnderwijsOnderdelenDto>(vak);
    }

    public async Task KoppelCursus(Guid vakGroupId, Guid cursusGroupId)
    {
        var vakken = await _vakRepository.GetList(x => x.GroupId == vakGroupId);
        var cursus = await _cursusRepository.GetLatestByGroupId(cursusGroupId);
        foreach (var vak in vakken)
        {
            if (!vak.Cursussen.Contains(cursus))
            {
                vak.Cursussen.Add(cursus);
                vak.RelationshipChanged = true;

                await _vakRepository.Update(vak);
            }
        }
    }

    public async Task KoppelLeeruitkomst(Guid vakGroupId, Guid lukGroupId)
    {
        var vakken = await _vakRepository.GetList(x => x.GroupId == vakGroupId);
        var luk = await _leeruitkomstRepository.GetLatestByGroupId(lukGroupId);
        foreach (var vak in vakken)
        {
            if (!vak.Leeruitkomsten.Contains(luk))
            {
                vak.Leeruitkomsten.Add(luk);
                vak.RelationshipChanged = true;

                await _vakRepository.Update(vak);
            }
        }
    }
}
