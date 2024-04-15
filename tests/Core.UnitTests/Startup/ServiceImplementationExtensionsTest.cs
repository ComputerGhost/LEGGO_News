using Core.Startup;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.UnitTests.Startup;

[TestClass]
public class ServiceImplementationExtensionsTest
{
    // Cached so we don't have to keep assembly scanning.
    private static ServiceProvider? _cachedServiceProvider;
    private ServiceProvider _serviceProvider = null!;

    [TestInitialize]
    public void Initialize()
    {
        _cachedServiceProvider ??= new ServiceCollection()
            .AddServiceImplementations(Assembly.GetExecutingAssembly())
            .BuildServiceProvider();
        _serviceProvider = _cachedServiceProvider;
    }

    private interface IUnregistered { };
    private class Unregistered : IUnregistered { }

    [TestMethod]
    public void WhenNotMarkedAsImplementation_DoesNotRegisterService()
    {
        // Arrange
        var serviceType = typeof(IUnregistered);

        // Act
        var implementation = _serviceProvider.GetService(serviceType);

        // Assert
        Assert.IsNull(implementation);
    }

    private interface IRegistered { }
    [ServiceImplementation]
    private class Registered : IRegistered { }

    [TestMethod]
    public void WhenImplementsOneService_RegistersService()
    {
        // Arrange
        var serviceType = typeof(IRegistered);
        var implementationType = typeof(Registered);

        // Act
        var implementation = _serviceProvider.GetService(serviceType);

        // Assert
        Assert.IsInstanceOfType(implementation, implementationType);
    }

    private interface IFirst { }
    private interface ISecond { }
    [ServiceImplementation(Interface = typeof(IFirst))]
    [ServiceImplementation(Interface = typeof(ISecond))]
    private class Multiple : IFirst, ISecond { }

    [TestMethod]
    public void WhenImplementsTwoServices_RegistersBoth()
    {
        // Arrange
        var firstServiceType = typeof(IFirst);
        var secondServiceType = typeof(ISecond);
        var implementationType = typeof(Multiple);

        // Act
        var firstImplementation = _serviceProvider.GetService(firstServiceType);
        var secondImplementation = _serviceProvider.GetService(secondServiceType);

        // Assert
        Assert.IsInstanceOfType(firstImplementation, implementationType);
        Assert.IsInstanceOfType(secondImplementation, implementationType);
    }

}
