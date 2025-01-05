using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Services.Base;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class LeeruitkomstService : VersionableServiceBase<Leeruitkomst, LeeruitkomstDto, MaakLeeruitkomstDto, UpdateLeeruitkomstDto>, ILeeruitkomstService
{
    private readonly ILeeruitkomstRepository _leeruitkomstRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateLeeruitkomstDto> _updateValidator;

    public LeeruitkomstService(ILeeruitkomstRepository leeruitkomstRepository,
                               IMapper mapper,
                               IValidator<UpdateLeeruitkomstDto> updateValidator,
                               IValidator<MaakLeeruitkomstDto> createValidator) : base(leeruitkomstRepository, mapper, createValidator)
    {
        _leeruitkomstRepository = leeruitkomstRepository;
        _mapper = mapper;
        _updateValidator = updateValidator;
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

    public override async Task<bool> Update(UpdateLeeruitkomstDto request)
    {
        _updateValidator.ValidateAndThrow(request);

        var dbLuk = await _leeruitkomstRepository.NieuwsteVoorGroepId(request.GroupId);
        if (dbLuk is null)
        {
            return false;
        }

        dbLuk.Naam = request.Naam;
        dbLuk.Beschrijving = request.Beschrijving;

        return await _leeruitkomstRepository.Update(dbLuk);
    }
}
