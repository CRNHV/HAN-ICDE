using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Dto.Vak;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class OpleidingService : IOpleidingService
{
    private readonly IOpleidingRepository _opleidingRepository;
    private readonly IVakRepository _vakRepository;

    public OpleidingService(IOpleidingRepository opleidingRepository, IVakRepository vakRepository)
    {
        _opleidingRepository = opleidingRepository;
        _vakRepository = vakRepository;
    }

    public async Task<List<OpleidingDto>> GetAllUnique()
    {
        var opleidingen = await _opleidingRepository.GetList();
        return opleidingen.ConvertAll(x => new OpleidingDto
        {
            Beschrijving = x.Beschrijving,
            GroupId = x.GroupId,
            Naam = x.Naam,
        });
    }

    public async Task<bool> KoppelVakAanOpleiding(Guid opleidingGroupId, Guid vakGroupId)
    {
        Opleiding opleiding = await _opleidingRepository.GetLatestByGroupId(opleidingGroupId);
        var vak = await _vakRepository.GetLatestByGroupId(vakGroupId);

        opleiding.Vakken.Add(vak);
        opleiding.RelationshipChanged = true;

        await _opleidingRepository.Update(opleiding);

        return true;
    }

    public async Task<OpleidingMetEerdereVersiesDto> ZoekOpleidingMetEerdereVersies(Guid groupId)
    {
        var result = new OpleidingMetEerdereVersiesDto();

        Opleiding opleiding = await _opleidingRepository.GetLatestByGroupId(groupId);
        var eerdereVersies = await _opleidingRepository.GetList(x => x.GroupId == groupId && x.Id != opleiding.Id);

        return new OpleidingMetEerdereVersiesDto()
        {
            OpleidingDto = new OpleidingDto()
            {
                Naam = opleiding.Naam,
                Beschrijving = opleiding.Beschrijving,
                GroupId = opleiding.GroupId,
                VersieNummer = opleiding.VersieNummer,
                Vakken = opleiding.Vakken.ConvertAll(x => new VakDto
                {
                    Naam = x.Naam,
                    Beschrijving = x.Beschrijving,
                })
            },
            EerdereVersies = eerdereVersies.ConvertAll(x => new OpleidingDto()
            {
                Naam = x.Naam,
                Beschrijving = x.Beschrijving,
                GroupId = x.GroupId,
                VersieNummer = x.VersieNummer,
            })
        };
    }
}
