using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Services.Base;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class OpleidingService : VersionableServiceBase<Opleiding, OpleidingDto, MaakOpleidingDto, UpdateOpleidingDto>, IOpleidingService
{
    private readonly IOpleidingRepository _opleidingRepository;
    private readonly IVakRepository _vakRepository;
    private readonly IMapper _mapper;

    public OpleidingService(IOpleidingRepository opleidingRepository, IVakRepository vakRepository, IMapper mapper) : base(opleidingRepository, mapper)
    {
        _opleidingRepository = opleidingRepository;
        _vakRepository = vakRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Kopie(Guid opleidingGroupId)
    {
        var opleiding = await _opleidingRepository.NieuwsteVoorGroepId(opleidingGroupId);
        if (opleiding is null)
        {
            return Guid.Empty;
        }

        var opleidingCopy = (Opleiding)opleiding.Clone();
        opleidingCopy.Naam += $" | KOPIE {DateTime.Now.ToShortDateString()}";

        opleidingCopy.GroupId = Guid.NewGuid();
        var createdOpleiding = await _opleidingRepository.Maak(opleidingCopy);
        if (createdOpleiding is null)
        {
            return Guid.Empty;
        }

        return createdOpleiding.GroupId;
    }

    public async Task<OpleidingDto?> Maak(MaakOpleidingDto request)
    {
        var opleiding = _mapper.Map<Opleiding>(request);
        opleiding.GroupId = Guid.NewGuid();
        var result = await _opleidingRepository.Maak(opleiding);
        if (result is null)
        {
            return null;
        }

        return _mapper.Map<OpleidingDto>(result);
    }

    public async Task<bool> KoppelVakAanOpleiding(Guid opleidingGroupId, Guid vakGroupId)
    {
        var opleiding = await _opleidingRepository.NieuwsteVoorGroepId(opleidingGroupId);
        if (opleiding is null)
        {
            return false;
        }

        var vak = await _vakRepository.NieuwsteVoorGroepId(vakGroupId);
        if (vak is null)
        {
            return false;
        }

        opleiding.Vakken.Add(vak);
        opleiding.RelationshipChanged = true;

        await _opleidingRepository.Update(opleiding);

        return true;
    }

    public async Task<OpleidingMetEerdereVersiesDto?> ZoekOpleidingMetEerdereVersies(Guid groupId)
    {
        var result = new OpleidingMetEerdereVersiesDto();

        var opleiding = await _opleidingRepository.NieuwsteVoorGroepId(groupId);
        if (opleiding is null)
        {
            return null;
        }

        var eerdereVersies = await _opleidingRepository.Lijst(x => x.GroupId == groupId && x.Id != opleiding.Id);
        return new OpleidingMetEerdereVersiesDto()
        {
            OpleidingDto = _mapper.Map<OpleidingMetVakkenDto>(opleiding),
            EerdereVersies = _mapper.Map<List<OpleidingDto>>(eerdereVersies)
        };
    }

    public override Task<Guid> MaakKopie(Guid groupId, int versieNummer)
    {
        throw new NotImplementedException();
    }

    public override async Task<bool> Update(UpdateOpleidingDto request)
    {
        var opleidingToUpdate = await _opleidingRepository.NieuwsteVoorGroepId(request.GroupId);
        if (opleidingToUpdate is null)
        {
            return false;
        }

        opleidingToUpdate.Naam = request.Naam;
        opleidingToUpdate.Beschrijving = request.Beschrijving;

        await _opleidingRepository.Update(opleidingToUpdate);
        var updatedOpleiding = await _opleidingRepository.NieuwsteVoorGroepId(request.GroupId);
        updatedOpleiding.RelationshipChanged = true;
        updatedOpleiding.Vakken = opleidingToUpdate.Vakken;

        await _opleidingRepository.Update(updatedOpleiding);
        return true;
    }
}
