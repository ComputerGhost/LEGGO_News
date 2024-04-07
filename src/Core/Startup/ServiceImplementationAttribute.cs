﻿using Microsoft.Extensions.DependencyInjection;

namespace Core.Startup;
internal class ServiceImplementationAttribute : Attribute
{
    /// <remarks>
    /// If null, class must have only one interface.
    /// </remarks>
    public Type? Interface { get; set; } = null;

    public ServiceLifetime Lifetime { get; set; }

    internal Type GetInterface(Type implementation)
    {
        if (Interface is not null)
        {
            return Interface;
        }

        var inherits = implementation.GetInterfaces();
        if (inherits.Length == 1)
        {
            return inherits[0];
        }

        var message = "Service interface is not defined and cannot be deduced.";
        var paramName = nameof(Interface);
        throw new ArgumentException(message, paramName);
    }
}
