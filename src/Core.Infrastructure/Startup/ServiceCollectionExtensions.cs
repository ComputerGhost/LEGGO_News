using Core.Domain.Startup;
using Core.Infrastructure.Database;
using Core.Infrastructure.Startup;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Infrastructure;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<InfrastructureOptions> configure)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services
            .Configure(configure)
            .AddMyDatabase()
            .AddServiceImplementations(assembly);
    }
}
