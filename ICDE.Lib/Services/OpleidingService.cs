using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class OpleidingService : IOpleidingService
{
    private readonly IOpleidingRepository _opleidingRepository;
    private readonly IVakRepository _vakRepository;
    private readonly IMapper _mapper;

    public OpleidingService(IOpleidingRepository opleidingRepository, IVakRepository vakRepository, IMapper mapper)
    {
        _opleidingRepository = opleidingRepository;
        _vakRepository = vakRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Kopie(Guid opleidingGroupId)
    {
        var opleiding = await _opleidingRepository.GetLatestByGroupId(opleidingGroupId);
        if (opleiding is null)
        {
            return Guid.Empty;
        }

        var opleidingCopy = (Opleiding)opleiding.Clone();
        opleidingCopy.Naam += $" | KOPIE {DateTime.Now.ToShortDateString()}";

        opleidingCopy.GroupId = Guid.NewGuid();
        var createdOpleiding = await _opleidingRepository.Create(opleidingCopy);
        if (createdOpleiding is null)
        {
            return Guid.Empty;
        }

        return createdOpleiding.GroupId;
    }

    public async Task<OpleidingDto?> Maak(CreateOpleiding request)
    {
        var opleiding = _mapper.Map<Opleiding>(request);
        opleiding.GroupId = Guid.NewGuid();
        var result = await _opleidingRepository.Create(opleiding);
        if (result is null)
        {
            return null;
        }

        return _mapper.Map<OpleidingDto>(result);
    }

    public async Task<bool> Verwijder(Guid groupId, int versie)
    {
        var opleidingen = await _opleidingRepository.GetList(x => x.GroupId == groupId && x.VersieNummer == versie);
        foreach (var item in opleidingen)
        {
            await _opleidingRepository.Delete(item);
        }

        return true;
    }

    public async Task<List<OpleidingDto>> HaalUniekeOp()
    {
        var opleidingen = await _opleidingRepository.GetList();

        return _mapper.Map<List<OpleidingDto>>(opleidingen);
    }

    public async Task<bool> KoppelVakAanOpleiding(Guid opleidingGroupId, Guid vakGroupId)
    {
        var opleiding = await _opleidingRepository.GetLatestByGroupId(opleidingGroupId);
        if (opleiding is null)
        {
            return false;
        }

        var vak = await _vakRepository.GetLatestByGroupId(vakGroupId);
        if (vak is null)
        {
            return false;
        }

        opleiding.Vakken.Add(vak);
        opleiding.RelationshipChanged = true;

        await _opleidingRepository.Update(opleiding);

        return true;
    }

    public async Task<bool> Update(UpdateOpleiding request)
    {
        var opleidingToUpdate = await _opleidingRepository.GetLatestByGroupId(request.GroupId);
        if (opleidingToUpdate is null)
        {
            return false;
        }

        opleidingToUpdate.Naam = request.Naam;
        opleidingToUpdate.Beschrijving = request.Beschrijving;

        await _opleidingRepository.Update(opleidingToUpdate);
        var updatedOpleiding = await _opleidingRepository.GetLatestByGroupId(request.GroupId);
        updatedOpleiding.RelationshipChanged = true;
        updatedOpleiding.Vakken = opleidingToUpdate.Vakken;

        await _opleidingRepository.Update(updatedOpleiding);
        return true;
    }

    public async Task<OpleidingMetEerdereVersiesDto?> ZoekOpleidingMetEerdereVersies(Guid groupId)
    {
        var result = new OpleidingMetEerdereVersiesDto();

        var opleiding = await _opleidingRepository.GetLatestByGroupId(groupId);
        if (opleiding is null)
        {
            return null;
        }

        var eerdereVersies = await _opleidingRepository.GetList(x => x.GroupId == groupId && x.Id != opleiding.Id);
        return new OpleidingMetEerdereVersiesDto()
        {
            OpleidingDto = _mapper.Map<OpleidingMetVakkenDto>(opleiding),
            EerdereVersies = _mapper.Map<List<OpleidingDto>>(eerdereVersies)
        };
    }
}
