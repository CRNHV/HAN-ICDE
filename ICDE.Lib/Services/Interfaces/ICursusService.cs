using ICDE.Lib.Dto.Cursus;

namespace ICDE.Lib.Services.Interfaces;
public interface ICursusService
{
    Task<List<CursusDto>> GetAll();
}
