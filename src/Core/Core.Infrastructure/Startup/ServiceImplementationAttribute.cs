using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Startup;
internal class ServiceImplementationAttribute : Attribute
{
    /// <remarks>
    /// If null, class must have only one interface.
    /// </remarks>
    public Type? Interface { get; set; } = null;

    public ServiceLifetime Lifetime { get; set; }

    internal Type GetInterface(Type implementation)
    {
        var inherits = implementation.GetInterfaces();
        if (inherits.Length == 1)
        {
            return inherits[0];
        }
        else
        {
            var message = "Service interface is not defined and cannot be deduced.";
            var paramName = nameof(Interface);
            throw new ArgumentException(message, paramName);
        }
    }
}
