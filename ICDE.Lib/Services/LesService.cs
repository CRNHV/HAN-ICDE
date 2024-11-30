using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Data.Repositories.Luk;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class LesService : ILesService
{
    private readonly ILesRepository _lesRepository;
    private readonly ILeeruitkomstRepository _leeruitkomstRepository;

    public LesService(ILesRepository lesRepository, ILeeruitkomstRepository leeruitkomstRepository)
    {
        _lesRepository = lesRepository;
        _leeruitkomstRepository = leeruitkomstRepository;
    }

    public async Task<LesDto> CreateLesson(string naam, string beschrijving)
    {
        var result = await _lesRepository.Create(new Les()
        {
            Naam = naam,
            Beschrijving = beschrijving,
            GroupId = Guid.NewGuid(),
        });

        return new LesDto()
        {
            Beschrijving = result.Beschrijving,
            Id = result.Id,
            Naam = result.Naam,
            GroupId = result.GroupId,
            VersieNummer = result.VersieNummer,
        };
    }

    public async Task<List<LesDto>> GetAllUniqueLessons()
    {
        var lessons = await _lesRepository.GetList();
        return lessons.ConvertAll(x => new LesDto()
        {
            Beschrijving = x.Beschrijving,
            GroupId = x.GroupId,
            Id = x.Id,
            Naam = x.Naam,
            VersieNummer = x.VersieNummer
        });
    }

    public async Task<LesMetEerdereVersies> GetLessonWithPreviousVersions(Guid groupId)
    {
        var currentVersion = await _lesRepository.GetLatestByGroupId(groupId);
        var otherVersions = await _lesRepository.GetList(x => x.GroupId == groupId && x.Id != currentVersion.Id);

        return new LesMetEerdereVersies()
        {
            Les = new LesDto()
            {
                Beschrijving = currentVersion.Beschrijving,
                GroupId = currentVersion.GroupId,
                Id = currentVersion.Id,
                Naam = currentVersion.Naam,
                VersieNummer = currentVersion.VersieNummer
            },
            LesList = otherVersions.ConvertAll(x => new LesDto()
            {
                Beschrijving = x.Beschrijving,
                GroupId = x.GroupId,
                Id = x.Id,
                Naam = x.Naam,
                VersieNummer = x.VersieNummer
            }),
            LesLeeruitkomsten = currentVersion.Leeruitkomsten.ConvertAll(x => new LeeruitkomstDto()
            {
                Beschrijving = x.Beschrijving,
                GroupId = x.GroupId,
                Id = x.Id,
                Naam = x.Naam,
                VersieNummer = x.VersieNummer
            })
        };
    }

    public async Task KoppelLukAanLes(Guid lesGroupId, Guid lukGroupId)
    {
        var lessen = await _lesRepository.GetLessonsWithLearningGoals(lesGroupId);
        var luk = await _leeruitkomstRepository.GetLatestByGroupId(lukGroupId);

        foreach (var item in lessen)
        {
            if (item.Leeruitkomsten.Contains(luk))
                continue;

            item.Leeruitkomsten.Add(luk);
            item.RelationshipChanged = true;
            await _lesRepository.Update(item);
        }
    }

    public async Task OntkoppelLukAanLes(Guid lesGroupId, Guid lukGroupId)
    {
        var lessen = await _lesRepository.GetLessonsWithLearningGoals(lesGroupId);
        foreach (var item in lessen)
        {
            item.Leeruitkomsten.RemoveAll(x => x.GroupId == lukGroupId);
            item.RelationshipChanged = true;
            await _lesRepository.Update(item);
        }
    }
}
