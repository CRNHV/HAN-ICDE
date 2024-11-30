using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Dto.Vak;

namespace ICDE.Lib.AutoMapper;
internal class AutoMapperConfigs : Profile
{
    public AutoMapperConfigs()
    {
        CreateCursusMappings();
        CreateSimpleDtoMappings();
        CreateVakMappings();
        CreateOpleidingMappings();
        CreatePlanningMappings();
    }

    private void CreateVakMappings()
    {
        CreateMap<Vak, VakDto>().ReverseMap();
        CreateMap<Vak, VakMetOnderwijsOnderdelenDto>();
    }
    private void CreateOpleidingMappings()
    {
        CreateMap<Opleiding, OpleidingDto>().ReverseMap();
        CreateMap<Opleiding, OpleidingMetVakkenDto>();
    }
    private void CreatePlanningMappings()
    {
        CreateMap<Planning, PlanningDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.PlanningItems));

        CreateMap<PlanningItem, PlanningItemDto>()
            .ForMember(dest => dest.Index, opt => opt.MapFrom(src => src.Index))
            .ForMember(dest => dest.PlanningItemNaam, opt => opt.MapFrom(src =>
                src.Les != null ? $"Les: {src.Les.Naam}" : $"Opdracht: {src.Opdracht.Naam}"));
    }
    private void CreateCursusMappings()
    {
        CreateMap<Cursus, CursusDto>().ReverseMap();

        CreateMap<Cursus, CursusMetPlanningDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Beschrijving, opt => opt.MapFrom(src => src.Beschrijving))
            .ForMember(dest => dest.Naam, opt => opt.MapFrom(src => src.Naam))
            .ForMember(dest => dest.Leeruitkomsten, opt => opt.MapFrom(src => src.Leeruitkomsten))
            .ForMember(dest => dest.Planning, opt => opt.MapFrom(src => src.Planning));
    }
    private void CreateSimpleDtoMappings()
    {
        CreateMap<Leeruitkomst, LeeruitkomstDto>().ReverseMap();
        CreateMap<Les, LesDto>().ReverseMap();
        CreateMap<Opdracht, OpdrachtDto>();
    }
}
