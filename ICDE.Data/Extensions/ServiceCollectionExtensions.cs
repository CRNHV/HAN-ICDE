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

        return services;
    }
}
