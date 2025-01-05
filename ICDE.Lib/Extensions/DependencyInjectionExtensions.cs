using System.Reflection;
using FluentValidation;
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
using ICDE.Lib.Identity;
using ICDE.Lib.IO;
using ICDE.Lib.Reports;
using ICDE.Lib.Services;
using ICDE.Lib.Services.Interfaces;
using ICDE.Lib.Validation.Dto.BeoordelingCriterea;
using ICDE.Lib.Validation.Dto.Cursus;
using ICDE.Lib.Validation.Dto.Leeruitkomst;
using ICDE.Lib.Validation.Dto.Lessen;
using ICDE.Lib.Validation.Dto.Opdracht;
using ICDE.Lib.Validation.Dto.OpdrachtBeoordeling;
using ICDE.Lib.Validation.Dto.OpdrachtInzending;
using ICDE.Lib.Validation.Dto.Opleiding;
using ICDE.Lib.Validation.Dto.Planning;
using ICDE.Lib.Validation.Dto.Vak;
using ICDE.Lib.Validation.Leeruitkomsten;
using Microsoft.Extensions.DependencyInjection;

namespace ICDE.Lib.Extensions;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddLib(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IFileManager, FileManager>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IOpdrachtService, OpdrachtService>();
        services.AddScoped<ILeeruitkomstService, LeeruitkomstService>();
        services.AddScoped<ILesService, LesService>();
        services.AddScoped<IVakService, VakService>();
        services.AddScoped<ICursusService, CursusService>();
        services.AddScoped<IOpleidingService, OpleidingService>();
        services.AddScoped<IPlanningService, PlanningService>();
        services.AddScoped<IRapportageService, RapportageService>();
        services.AddTransient<IValidationManager, ValidationManager>();
        services.AddScoped<IIngeleverdeOpdrachtService, IngeleverdeOpdrachtService>();
        services.AddScoped<IBeoordelingCritereaService, BeoordelingCritereaService>();
        services.AddScoped<IOpdrachtBeoordelingService, OpdrachtBeoordelingService>();
        services.AddScoped<IReportExporter, PdfReportExporter>();

        services.AddKeyedTransient<IReportGenerator, CursusReportGenerator>("CursusReportGenerator");
        services.AddKeyedTransient<IReportGenerator, VakReportGenerator>("VakReportGenerator");
        services.AddKeyedTransient<IReportGenerator, OpleidingReportGenerator>("OpleidingReportGenerator");

        services.AddScoped<IValidator<MaakBeoordelingCritereaDto>, MaakBeoordelingCritereaValidation>();
        services.AddScoped<IValidator<UpdateBeoordelingCritereaDto>, UpdateBeoordelingCritereaValidation>();
        services.AddScoped<IValidator<MaakCursusDto>, MaakCursusValidation>();
        services.AddScoped<IValidator<UpdateCursusDto>, UpdateCursusValidation>();
        services.AddScoped<IValidator<MaakLeeruitkomstDto>, MaakLeeruitkomstValidation>();
        services.AddScoped<IValidator<UpdateLeeruitkomstDto>, UpdateLeeruitkomstValidation>();
        services.AddScoped<IValidator<MaakLesDto>, MaakLesValidation>();
        services.AddScoped<IValidator<UpdateLesDto>, UpdateLesValidation>();
        services.AddScoped<IValidator<MaakOpdrachtDto>, MaakOpdrachtValidation>();
        services.AddScoped<IValidator<UpdateOpdrachtDto>, UpdateOpdrachtValidation>();
        services.AddScoped<IValidator<OpdrachtBeoordelingDto>, OpdrachtBeoordelingValidation>();
        services.AddScoped<IValidator<LeverOpdrachtInDto>, OpdrachtInzendingValidation>();
        services.AddScoped<IValidator<MaakOpleidingDto>, MaakOpleidingValidation>();
        services.AddScoped<IValidator<UpdateOpleidingDto>, UpdateOpleidingValidation>();
        services.AddScoped<IValidator<MaakPlanningDto>, MaakPlanningValidation>();
        services.AddScoped<IValidator<UpdatePlanningDto>, UpdatePlanningValidation>();
        services.AddScoped<IValidator<MaakVakDto>, MaakVakValidation>();
        services.AddScoped<IValidator<UpdateVakDto>, UpdateVakValidation>();

        services.AddScoped<ISignInManager, SignInManager>();

        return services;
    }
}
