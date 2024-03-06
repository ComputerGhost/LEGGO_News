using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Application.Startup;
public class CoreServiceCollectionExtensions
{
    public static void AddCore(this IServiceCollection services, Action<CoreOptions> configure)
    {
        services
            .Configure(configure)
            .AddLibraries()
            .AddOurServices();
    }

    private static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

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
