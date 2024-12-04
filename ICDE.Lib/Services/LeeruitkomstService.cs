using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Luk;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class LeeruitkomstService : ILeeruitkomstService
{
    private readonly ILeeruitkomstRepository _leeruitkomstRepository;
    private readonly IMapper _mapper;

    public LeeruitkomstService(ILeeruitkomstRepository leeruitkomstRepository, IMapper mapper)
    {
        _leeruitkomstRepository = leeruitkomstRepository;
        _mapper = mapper;
    }

    public async Task<LeeruitkomstMetEerdereVersiesDto> GetEntityWithEarlierVersions(Guid leeruitkomstId)
    {
        LeeruitkomstMetEerdereVersiesDto luk = new LeeruitkomstMetEerdereVersiesDto();

        var leeruitkomst = await _leeruitkomstRepository.GetLatestByGroupId(leeruitkomstId);
        var eerdereVersies = await _leeruitkomstRepository.GetList(x => x.GroupId == leeruitkomst.GroupId && x.Id != leeruitkomst.Id);

        luk.Leeruitkomst = _mapper.Map<LeeruitkomstDto>(leeruitkomst);
        luk.EerdereVersies = _mapper.Map<List<LeeruitkomstDto>>(eerdereVersies);
        return luk;
    }

    public async Task<List<LeeruitkomstDto>> GetAll()
    {
        var dbLuks = await _leeruitkomstRepository.GetList();
        return _mapper.Map<List<LeeruitkomstDto>>(dbLuks);
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

        return _mapper.Map<LeeruitkomstDto>(result);
    }

    public async Task<LeeruitkomstDto?> UpdateLeeruitkomst(LukUpdateDto request)
    {
        var leeruitkomstToUpdate = await _leeruitkomstRepository.Get(request.Id);
        if (leeruitkomstToUpdate is null)
            return null;

        leeruitkomstToUpdate.Naam = request.Naam;
        leeruitkomstToUpdate.Beschrijving = request.Beschrijving;

        var result = await _leeruitkomstRepository.Update(leeruitkomstToUpdate);
        return result != null ? _mapper.Map<LeeruitkomstDto>(result) : null;
    }

    public async Task<LeeruitkomstDto> GetVersion(Guid groupId, int versieId)
    {
        var dbLuks = await _leeruitkomstRepository.GetList(x => x.GroupId == groupId && x.VersieNummer == versieId);
        var luk = dbLuks.FirstOrDefault();
        return _mapper.Map<LeeruitkomstDto>(luk);
    }
}
