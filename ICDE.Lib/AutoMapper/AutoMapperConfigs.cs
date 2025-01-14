using AutoMapper;
using ICDE.Data.Entities;
using ICDE.Lib.Dto.BeoordelingCriterea;
using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Dto.Leeruitkomst;
using ICDE.Lib.Dto.Lessen;
using ICDE.Lib.Dto.Opdracht;
using ICDE.Lib.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Dto.OpdrachtInzending;
using ICDE.Lib.Dto.Opleidingen;
using ICDE.Lib.Dto.Planning;
using ICDE.Lib.Dto.Vak;

namespace ICDE.Lib.AutoMapper;
internal class AutoMapperConfigs : Profile
{
    public AutoMapperConfigs()
    {
        CreateCursusMappings();
        CreateLukMappings();
        CreateVakMappings();
        CreateOpleidingMappings();
        CreatePlanningMappings();
        CreateOpdrachtMappings();
        CreateLesMappings();
        CreateBeoordelingCritereaMappings();
    }

    private void CreateBeoordelingCritereaMappings()
    {
        CreateMap<BeoordelingCriterea, BeoordelingCritereaDto>();
        CreateMap<MaakBeoordelingCritereaDto, BeoordelingCriterea>();
    }

    private void CreateOpdrachtMappings()
    {
        CreateMap<Opdracht, OpdrachtDto>()
            .ForCtorParam("isToets", opt => opt.MapFrom(x => x.Type == OpdrachtType.Toets));

        CreateMap<MaakOpdrachtDto, Opdracht>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.IsToets ? OpdrachtType.Toets : OpdrachtType.Casus));

        CreateMap<OpdrachtBeoordeling, OpdrachtMetBeoordelingDto>()
            .ForMember(dest => dest.OpdrachtNaam, opt => opt.MapFrom(src => src.IngeleverdeOpdracht.Opdracht.Naam));

        CreateMap<IngeleverdeOpdracht, IngeleverdeOpdrachtDto>();
        CreateMap<OpdrachtBeoordeling, OpdrachtBeoordelingDto>();
        CreateMap<BeoordelingCriterea, BeoordelingCritereaDto>();
    }

    private void CreateVakMappings()
    {
        CreateMap<Vak, VakDto>().ReverseMap();
        CreateMap<Vak, VakMetOnderwijsOnderdelenDto>();
        CreateMap<MaakVakDto, Vak>();
    }
    private void CreateOpleidingMappings()
    {
        CreateMap<Opleiding, OpleidingDto>().ReverseMap();
        CreateMap<Opleiding, OpleidingMetVakkenDto>();
        CreateMap<MaakOpleidingDto, Opleiding>();
    }
    private void CreatePlanningMappings()
    {

        CreateMap<Planning, PlanningZonderItemsDto>();
        CreateMap<Planning, PlanningDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.PlanningItems));

        CreateMap<PlanningItem, PlanningItemDto>()
            .ForMember(dest => dest.Index, opt => opt.MapFrom(src => src.Index))
            .ForMember(dest => dest.PlanningItemNaam, opt => opt.MapFrom(src =>
                src.Les != null ? $"Les: {src.Les.Naam}" : $"Opdracht: {src.Opdracht.Naam}"))
            .ForMember(dest => dest.Leeruitkomsten, opt =>
                opt.MapFrom(src => src.Les == null ?
                src.Opdracht.BeoordelingCritereas.SelectMany(x => x.Leeruitkomsten).Select(x => x.Naam).Distinct().ToList() :
                src.Les.Leeruitkomsten.Select(x => x.Naam).Distinct().ToList())
            );

        CreateMap<MaakPlanningDto, Planning>();
    }
    private void CreateCursusMappings()
    {
        CreateMap<Cursus, CursusDto>().ReverseMap();
        CreateMap<MaakCursusDto, Cursus>()
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(_ => Guid.NewGuid()));
        CreateMap<Cursus, CursusMetPlanningDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Beschrijving, opt => opt.MapFrom(src => src.Beschrijving))
            .ForMember(dest => dest.Naam, opt => opt.MapFrom(src => src.Naam))
            .ForMember(dest => dest.Leeruitkomsten, opt => opt.MapFrom(src => src.Leeruitkomsten))
            .ForMember(dest => dest.Planning, opt => opt.MapFrom(src => src.Planning));
    }
    private void CreateLesMappings()
    {
        CreateMap<Les, LesDto>().ReverseMap();
        CreateMap<Les, LesMetLeeruitkomstenDto>();
        CreateMap<MaakLesDto, Les>();
    }

    private void CreateLukMappings()
    {
        CreateMap<Leeruitkomst, LeeruitkomstDto>().ReverseMap();
    }
}
