using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Common.DependencyInjection;

public static class ServiceCollectionExtensions
{
    private static bool _configured = false;

    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        if (!_configured)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services
                .AddImplementations(assembly)
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            _configured = true;
        }

        return services;
    }

    private static IServiceCollection AddImplementations(this IServiceCollection services, Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            foreach (var attribute in type.GetCustomAttributes<ServiceImplementationAttribute>())
            {
                services.Add(new ServiceDescriptor(
                    attribute.GetInterface(type),
                    type,
                    attribute.Lifetime));
            }
        }

        return services;
    }
}
