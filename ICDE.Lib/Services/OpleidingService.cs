using AutoMapper;
using FluentValidation;
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
    private readonly IValidator<UpdateOpleidingDto> _updateValidator;

    public OpleidingService(IOpleidingRepository opleidingRepository,
                            IVakRepository vakRepository,
                            IMapper mapper,
                            IValidator<MaakOpleidingDto> createValidator,
                            IValidator<UpdateOpleidingDto> updateValidator) : base(opleidingRepository, mapper, createValidator)
    {
        _opleidingRepository = opleidingRepository;
        _vakRepository = vakRepository;
        _mapper = mapper;
        _updateValidator = updateValidator;
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

        return await _opleidingRepository.Update(opleiding);
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

    public override async Task<bool> Update(UpdateOpleidingDto request)
    {
        _updateValidator.ValidateAndThrow(request);

        var opleidingToUpdate = await _opleidingRepository.NieuwsteVoorGroepId(request.GroupId);
        if (opleidingToUpdate is null)
        {
            return false;
        }

        opleidingToUpdate.Naam = request.Naam;
        opleidingToUpdate.Beschrijving = request.Beschrijving;

        await _opleidingRepository.Update(opleidingToUpdate);
        var updatedOpleiding = await _opleidingRepository.NieuwsteVoorGroepId(request.GroupId);
        if (updatedOpleiding is null)
        {
            return false;
        }
        updatedOpleiding.RelationshipChanged = true;
        updatedOpleiding.Vakken = opleidingToUpdate.Vakken;

        return await _opleidingRepository.Update(updatedOpleiding);
    }
}
