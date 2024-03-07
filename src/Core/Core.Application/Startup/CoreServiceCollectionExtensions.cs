using Core.Infrastructure.Startup;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Application.Startup;
public static class CoreServiceCollectionExtensions
{
    public static void AddCore(this IServiceCollection services, Action<CoreOptions> configure)
    {
        services
            .AddInfrastructure(ConfigureInfrastructure(configure))
            .AddLibraries();
    }

    private static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        return services;
    }

    private static Action<InfrastructureOptions> ConfigureInfrastructure(Action<CoreOptions> configure)
    {
        return (InfrastructureOptions infrastructureOptions) =>
        {
            configure(new CoreOptions(infrastructureOptions));
        };
    }
}
