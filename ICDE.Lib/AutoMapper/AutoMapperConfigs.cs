using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Dto.Vak;

namespace ICDE.Lib.AutoMapper;
internal class AutoMapperConfigs : Profile
{
    public AutoMapperConfigs()
    {
        CreateDtoMappings();
    }

    public void CreateDtoMappings()
    {
        CreateMap<Cursus, CursusDto>().ReverseMap();
        CreateMap<Leeruitkomst, LeeruitkomstDto>().ReverseMap();
        CreateMap<Les, LesDto>().ReverseMap();
        CreateMap<Opdracht, OpdrachtDto>();
        CreateMap<Vak, VakDto>().ReverseMap();
        CreateMap<Opleiding, OpleidingDto>().ReverseMap();
    }
}
