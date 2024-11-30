using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Dto.Vak;
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

    public async Task<List<OpleidingDto>> GetAllUnique()
    {
        var opleidingen = await _opleidingRepository.GetList();

        return _mapper.Map<List<OpleidingDto>>(opleidingen);
        //return opleidingen.ConvertAll(x => new OpleidingDto
        //{
        //    Beschrijving = x.Beschrijving,
        //    GroupId = x.GroupId,
        //    Naam = x.Naam,
        //});
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
            OpleidingDto = _mapper.Map<OpleidingMetVakkenDto>(opleiding),
            EerdereVersies = _mapper.Map<List<OpleidingDto>>(eerdereVersies)
        };
    }
}
