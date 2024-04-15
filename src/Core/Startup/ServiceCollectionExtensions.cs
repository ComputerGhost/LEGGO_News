using Core.Common.Database;
using Core.Common.Ports;
using FluentValidation;
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
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
}
