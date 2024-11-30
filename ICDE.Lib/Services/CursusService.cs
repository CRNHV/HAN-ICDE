using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Domain;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class CursusService : ICursusService
{
    private readonly ICursusRepository _cursusRepository;

    public CursusService(ICursusRepository cursusRepository)
    {
        _cursusRepository = cursusRepository;
    }

    public async Task<List<CursusDto>> GetAll()
    {
        var cursussen = await _cursusRepository.GetList();
        return cursussen.ConvertAll(x => new CursusDto
        {
            Beschrijving = x.Beschrijving,
            Naam = x.Naam,
            GroupId = x.GroupId,
        });
    }

    public async Task<CursusMetPlanningDto> GetFullCursusByGroupId(Guid cursusGroupId)
    {
        var cursus = await _cursusRepository.GetFulLCursusData(cursusGroupId);
        List<PlanningItemDto> planningItems = new();
        if (cursus.Planning != null)
        {
            planningItems = cursus.Planning.PlanningItems.ConvertAll(x => PlanningItemMapper.MapPlanningItemToDto(x));
        }

        return new CursusMetPlanningDto()
        {
            Id = cursus.Id,
            Beschrijving = cursus.Beschrijving,
            Naam = cursus.Naam,
            Leeruitkomsten = cursus.Leeruitkomsten.ConvertAll(x => new LeeruitkomstDto
            {
                Beschrijving = x.Beschrijving,
                Naam = x.Naam,
                GroupId = x.GroupId,
            }),
            Planning = new PlanningDto()
            {
                Naam = cursus.Planning.Name,
                Items = planningItems,
            }
        };
    }

    public async Task<List<CursusDto>> GetEarlierVersionsByGroupId(Guid groupId, int exceptId)
    {
        var cursussen = await _cursusRepository.GetList(x => x.GroupId == groupId && x.Id != exceptId);
        return cursussen.ConvertAll(x => new CursusDto()
        {
            Beschrijving = x.Beschrijving,
            GroupId = x.GroupId,
            Naam = x.Naam,
            VersieNummer = x.VersieNummer,
        });
    }
}
