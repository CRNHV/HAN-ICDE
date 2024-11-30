using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Services.Interfaces;

namespace ICDE.Lib.Services;
internal class CursusService : ICursusService
{
    private readonly ICursusRepository _cursusRepository;

    public CursusService(ICursusRepository cursusRepository)
    {
        _cursusRepository = cursusRepository;
    }

    public async Task<List<CursusDto>> GetAll()
    {
        var cursussen = await _cursusRepository.GetList();
        return cursussen.ConvertAll(x => new CursusDto
        {
            Beschrijving = x.Beschrijving,
            Naam = x.Naam,
            GroupId = x.GroupId,
        });
    }
}
