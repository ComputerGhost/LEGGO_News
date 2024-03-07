using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Infrastructure.Startup;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<InfrastructureOptions> configure)
    {
        services
            .Configure(configure)
            .AddOurServices();
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
