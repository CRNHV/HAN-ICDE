using ICDE.Data.Repositories;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Data.Repositories.Luk;
using Microsoft.Extensions.DependencyInjection;

namespace ICDE.Data.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOpdrachtRepository, OpdrachtRepository>();
        services.AddScoped<ILeeruitkomstRepository, LeeruitkomstRepository>();
        services.AddScoped<ILesRepository, LesRepository>();
        services.AddScoped<IVakRepository, VakRepository>();
        services.AddScoped<ICursusRepository, CursusRepository>();
        services.AddScoped<IOpleidingRepository, OpleidingRepository>();
        services.AddScoped<IPlanningRepository, PlanningRepository>();
        services.AddScoped<IIngeleverdeOpdrachtRepository, IngeleverdeOpdrachtRepository>();
        services.AddScoped<IOpdrachtBeoordelingRepository, OpdrachtBeoordelingRepository>();
        services.AddScoped<IBeoordelingCritereaRepository, BeoordelingCritereaRepository>();

        return services;
    }
}
