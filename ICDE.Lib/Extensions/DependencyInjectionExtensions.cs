
using System.Reflection;
using ICDE.Lib.IO;
using ICDE.Lib.Services;
using ICDE.Lib.Services.Interfaces;
using ICDE.Lib.Validator;
using ICDE.Lib.Validator.Interfaces;
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

        return services;
    }
}
