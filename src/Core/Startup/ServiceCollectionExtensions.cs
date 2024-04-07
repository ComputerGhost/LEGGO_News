using Core.Common.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Core.Startup;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, Action<CoreOptions> configure)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return services
            .Configure(configure)
            .AddOurLibraries(assembly)
            .AddOurServices(assembly);
    }

    private static IServiceCollection AddOurLibraries(this IServiceCollection services, Assembly assembly)
    {
        services.AddDbContext<MyDbContext>(config =>
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IOptions<CoreOptions>>().Value;
            config.UseSqlServer(configuration.DatabaseConnectionString);
        });

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }

    private static IServiceCollection AddOurServices(this IServiceCollection services, Assembly assembly)
    {
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
