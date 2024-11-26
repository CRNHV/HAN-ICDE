using ICDE.Data.Entities.OnderwijsOnderdeel;
using ICDE.Data.Repositories.Luk;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services.Interfaces;
using ICDE.Lib.ViewModels;

namespace ICDE.Lib.Services;
internal class LeeruitkomstService : ILeeruitkomstService
{
    private readonly ILeeruitkomstRepository _leeruitkomstRepository;

    public LeeruitkomstService(ILeeruitkomstRepository leeruitkomstRepository)
    {
        _leeruitkomstRepository = leeruitkomstRepository;
    }

    public async Task<LeeruitkomstMetEerdereVersies> GetEntityWithEarlierVersions(Guid leeruitkomstId)
    {
        LeeruitkomstMetEerdereVersies luk = new LeeruitkomstMetEerdereVersies();

        var leeruitkomst = await _leeruitkomstRepository.GetLatestByGroupId(leeruitkomstId);
        luk.Leeruitkomst = new LeeruitkomstDto()
        {
            Id = leeruitkomst.Id,
            Beschrijving = leeruitkomst!.Beschrijving,
            GroupId = leeruitkomst.GroupId,
            Naam = leeruitkomst.Naam,
        };

        var eerdereVersies = await _leeruitkomstRepository.GetList(x => x.GroupId == leeruitkomst.GroupId && x.Id != leeruitkomst.Id);
        luk.EerdereVersies = eerdereVersies.ConvertAll(x => new LeeruitkomstDto
        {
            Id = x.Id,
            GroupId = x.GroupId,
            Beschrijving = x.Beschrijving,
            Naam = x.Naam,
            VersieNummer = x.VersieNummer,
        });

        return luk;
    }

    public async Task<List<LeeruitkomstDto>> GetAll()
    {
        var dbLuks = await _leeruitkomstRepository.GetList();
        return dbLuks.ConvertAll(x => new LeeruitkomstDto
        {
            Id = x.Id,
            Beschrijving = x.Beschrijving,
            Naam = x.Naam,
            GroupId = x.GroupId,
        });
    }

    public async Task<LeeruitkomstDto?> MaakLeeruitkomst(MaakLeeruitkomstDto request)
    {
        var result = await _leeruitkomstRepository.Create(new Leeruitkomst()
        {
            Beschrijving = request.Beschrijving,
            Naam = request.Naam,
            VersieNummer = 0,
            GroupId = Guid.NewGuid(),
        });

        if (result is null)
            return null;

        return new LeeruitkomstDto()
        {
            Beschrijving = result.Beschrijving,
            Naam = result.Naam,
            GroupId = result.GroupId,
            VersieNummer = result.VersieNummer,
        };
    }

    public async Task<LeeruitkomstDto?> UpdateLeeruitkomst(LukUpdateDto request)
    {
        var leeruitkomstToUpdate = await _leeruitkomstRepository.Get(request.Id);
        if (leeruitkomstToUpdate is null)
            return null;

        leeruitkomstToUpdate.Naam = request.Naam;
        leeruitkomstToUpdate.Beschrijving = request.Beschrijving;

        var result = await _leeruitkomstRepository.Update(leeruitkomstToUpdate);
        return result != null ? new LeeruitkomstDto()
        {
            Beschrijving = result.Beschrijving,
            Naam = result.Naam,
            GroupId = result.GroupId,
            Id = result.Id,
        } : null;
    }

    public async Task<LeeruitkomstDto> GetVersion(Guid groupId, int versieId)
    {
        var dbLuks = await _leeruitkomstRepository.GetList(x => x.GroupId == groupId && x.VersieNummer == versieId);
        var luk = dbLuks.FirstOrDefault();
        return new LeeruitkomstDto
        {
            Id = luk.Id,
            Beschrijving = luk.Beschrijving,
            Naam = luk.Naam,
            GroupId = luk.GroupId,
        };
    }
}
