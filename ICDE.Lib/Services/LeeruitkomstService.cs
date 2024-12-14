using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Luk;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services.Base;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class LeeruitkomstService : VersionableServiceBase<Leeruitkomst, LeeruitkomstDto, MaakLeeruitkomstDto, UpdateLeeruitkomstDto>, ILeeruitkomstService
{
    private readonly ILeeruitkomstRepository _leeruitkomstRepository;
    private readonly IMapper _mapper;

    public LeeruitkomstService(ILeeruitkomstRepository leeruitkomstRepository, IMapper mapper) : base(leeruitkomstRepository, mapper)
    {
        _leeruitkomstRepository = leeruitkomstRepository;
        _mapper = mapper;
    }

    public async Task<LeeruitkomstMetEerdereVersiesDto?> HaalOpMetEerdereVersies(Guid leeruitkomstId)
    {
        LeeruitkomstMetEerdereVersiesDto luk = new();

        var leeruitkomst = await _leeruitkomstRepository.NieuwsteVoorGroepId(leeruitkomstId);
        if (leeruitkomst is null)
        {
            return null;
        }
        luk.Leeruitkomst = _mapper.Map<LeeruitkomstDto>(leeruitkomst);

        var eerdereVersies = await _leeruitkomstRepository.Lijst(x => x.GroupId == leeruitkomst.GroupId && x.Id != leeruitkomst.Id);
        if (eerdereVersies.Count > 0)
        {
            luk.EerdereVersies = _mapper.Map<List<LeeruitkomstDto>>(eerdereVersies);
        }

        return luk;
    }

    public override Task<Guid> MaakKopie(Guid groupId, int versieNummer)
    {
        throw new NotImplementedException();
    }

    public override Task<bool> Update(UpdateLeeruitkomstDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> MaakKopieVanVersie(Guid groupId, int versieId)
    {
        var dbLuks = await _leeruitkomstRepository.Lijst(x => x.GroupId == groupId && x.VersieNummer == versieId);
        var luk = dbLuks.First();

        var lukClone = (Leeruitkomst)luk.Clone();
        lukClone.GroupId = Guid.NewGuid();
        await _leeruitkomstRepository.Maak(lukClone);
        return lukClone.GroupId;
    }
}
