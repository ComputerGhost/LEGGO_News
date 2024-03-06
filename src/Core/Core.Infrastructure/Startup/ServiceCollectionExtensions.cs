using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Startup;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, Action<InfrastructureOptions> configure)
    {
        services.Configure(configure);
    }
}
