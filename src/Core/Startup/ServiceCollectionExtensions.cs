using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Startup;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, Action<CoreOptions> configure)
    {
        return services
            .Configure(configure)
            .AddOurLibraries()
            .AddOurServices();
    }

    private static IServiceCollection AddOurLibraries(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }

    private static IServiceCollection AddOurServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            foreach (var attribute in type.GetCustomAttributes<ServiceImplementationAttribute>())
            {
                var service = attribute.GetInterface(type);
                var implementation = type;
                services.Add(new ServiceDescriptor(service, implementation, attribute.Lifetime));
            }
        }

        return services;
    }
}
