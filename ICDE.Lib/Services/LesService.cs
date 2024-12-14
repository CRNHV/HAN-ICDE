using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Data.Repositories.Luk;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Services.Base;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class LesService : VersionableServiceBase<Les, LesDto, MaakLesDto, UpdateLesDto>, ILesService
{
    private readonly ILesRepository _lesRepository;
    private readonly ILeeruitkomstRepository _leeruitkomstRepository;
    private readonly IMapper _mapper;

    public LesService(ILesRepository lesRepository, ILeeruitkomstRepository leeruitkomstRepository, IMapper mapper) : base(lesRepository, mapper)
    {
        _lesRepository = lesRepository;
        _leeruitkomstRepository = leeruitkomstRepository;
        _mapper = mapper;
    }

    public async Task<LesDto?> Maak(string naam, string beschrijving)
    {
        var result = await _lesRepository.Maak(new Les()
        {
            Naam = naam,
            Beschrijving = beschrijving,
            GroupId = Guid.NewGuid(),
        });

        if (result is null)
        {
            return null;
        }

        return _mapper.Map<LesDto>(result);
    }

    public async Task<LesMetEerdereVersies?> HaalLessenOpMetEerdereVersies(Guid groupId)
    {
        var currentVersion = await _lesRepository.NieuwsteVoorGroepId(groupId);
        if (currentVersion is null)
        {
            return null;
        }

        var otherVersions = await _lesRepository.Lijst(x => x.GroupId == groupId && x.Id != currentVersion.Id);
        return new LesMetEerdereVersies()
        {
            Les = _mapper.Map<LesDto>(currentVersion),
            LesList = _mapper.Map<List<LesDto>>(otherVersions),
            LesLeeruitkomsten = _mapper.Map<List<LeeruitkomstDto>>(currentVersion.Leeruitkomsten)
        };
    }

    public async Task<bool> KoppelLukAanLes(Guid lesGroupId, Guid lukGroupId)
    {
        var lessen = await _lesRepository.GetLessonsWithLearningGoals(lesGroupId);
        if (lessen.Count == 0)
        {
            return false;
        }

        var luk = await _leeruitkomstRepository.NieuwsteVoorGroepId(lukGroupId);
        if (luk is null)
        {
            return false;
        }

        foreach (var item in lessen)
        {
            if (item.Leeruitkomsten.Contains(luk))
                continue;

            item.Leeruitkomsten.Add(luk);
            item.RelationshipChanged = true;
            await _lesRepository.Update(item);
        }

        return true;
    }

    public async Task<bool> OntkoppelLukAanLes(Guid lesGroupId, Guid lukGroupId)
    {
        var lessen = await _lesRepository.GetLessonsWithLearningGoals(lesGroupId);
        if (lessen.Count == 0)
        {
            return false;
        }

        foreach (var item in lessen)
        {
            item.Leeruitkomsten.RemoveAll(x => x.GroupId == lukGroupId);
            item.RelationshipChanged = true;
            await _lesRepository.Update(item);
        }

        return true;
    }

    public override async Task<Guid> MaakKopie(Guid groupId, int versieNummer)
    {
        throw new NotImplementedException();
    }

    public override async Task<bool> Update(UpdateLesDto request)
    {
        var les = await _lesRepository.NieuwsteVoorGroepId(request.GroupId);
        if (les is null)
        {
            return false;
        }

        les.Naam = request.Naam;
        les.Beschrijving = request.Beschrijving;
        await _lesRepository.Update(les);

        var updatedLes = await _lesRepository.NieuwsteVoorGroepId(request.GroupId);
        updatedLes.Leeruitkomsten = les.Leeruitkomsten;
        updatedLes.RelationshipChanged = true;
        await _lesRepository.Update(updatedLes);

        return true;
    }
}
