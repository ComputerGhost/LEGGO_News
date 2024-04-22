using Core.Common.Database;
using Core.Common.Imaging;
using FluentValidation;
using MediatR.Behaviors.Authorization.Extensions.DependencyInjection;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Startup;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, Action<CoreOptions> configure)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services
            .Configure(configure)
            .AddCQRS(assembly)
            .AddMyDatabase()
            .AddFileStorage()
            .AddServiceImplementations(assembly);
    }

    private static IServiceCollection AddCQRS(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);
        });

        services.AddFluentValidation([ assembly ]);

        services.AddMediatorAuthorization(assembly);
        services.AddAuthorizersFromAssembly(assembly);

        return services;
    }
}
