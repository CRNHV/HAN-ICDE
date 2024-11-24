﻿
using ICDE.Lib.IO;
using ICDE.Lib.Services;
using ICDE.Lib.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ICDE.Lib.Extensions;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddLib(this IServiceCollection services)
    {
        services.AddScoped<IFileManager, FileManager>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IOpdrachtService, OpdrachtService>();

        return services;
    }
}