using AutoMapper;
using FluentValidation;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class OpdrachtBeoordelingService : IOpdrachtBeoordelingService
{
    private readonly IOpdrachtBeoordelingRepository _beoordelingRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<OpdrachtBeoordelingDto> _opdrachtBeoordelingValidator;
    private readonly IIngeleverdeOpdrachtRepository _ingeleverdeOpdrachtRepository;

    public OpdrachtBeoordelingService(IOpdrachtBeoordelingRepository beoordelingRepository,
                                      IMapper mapper,
                                      IValidator<OpdrachtBeoordelingDto> opdrachtBeoordelingValidator,
                                      IIngeleverdeOpdrachtRepository ingeleverdeOpdrachtRepository)
    {
        _beoordelingRepository = beoordelingRepository;
        _mapper = mapper;
        _opdrachtBeoordelingValidator = opdrachtBeoordelingValidator;
        _ingeleverdeOpdrachtRepository = ingeleverdeOpdrachtRepository;
    }

    public async Task<List<OpdrachtMetBeoordelingDto>> HaalBeoordelingenOpVoorUser(int? userId)
    {
        if (userId is null)
        {
            return new List<OpdrachtMetBeoordelingDto>();
        }

        var beoordelingen = await _beoordelingRepository.HaalBeoordelingenOpVoorStudent(userId.Value);
        return _mapper.Map<List<OpdrachtMetBeoordelingDto>>(beoordelingen);
    }

    public async Task<bool> SlaBeoordelingOp(OpdrachtBeoordelingDto request)
    {
        _opdrachtBeoordelingValidator.ValidateAndThrow(request);

        var dbIngeleverdeOpdracht = await _ingeleverdeOpdrachtRepository.VoorId(request.InzendingId);
        if (dbIngeleverdeOpdracht is null)
            return false;

        await _beoordelingRepository.Maak(new OpdrachtBeoordeling()
        {
            Cijfer = request.Cijfer,
            Feedback = request.Feedback,
            IngeleverdeOpdracht = dbIngeleverdeOpdracht
        });

        return true;
    }
}
