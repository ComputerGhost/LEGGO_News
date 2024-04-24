using Core.Application.Common.Requirements;
using Core.Domain.Users.Enums;
using Core.Domain.Users.Ports;
using MediatR.Behaviors.Authorization;
using Moq;

namespace Core.Application.UnitTests.Common.Requirements;

[TestClass]
public class MustHaveRoleRequirementTests
{
    private Mock<ICurrentUserPort> _mockCurrentUserAdapter = null!;
    private IAuthorizationHandler<MustHaveRoleRequirement> _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockCurrentUserAdapter = new Mock<ICurrentUserPort>();

        _subject = new MustHaveRoleRequirement.Handler(_mockCurrentUserAdapter.Object);
    }

    [TestMethod]
    public async Task WhenUserHasRole_Passes()
    {
        // Arrange
        _mockCurrentUserAdapter
            .Setup(m => m.IsInRole(It.IsAny<UserRole>()))
            .Returns(true);

        // Act
        var requirement = new MustHaveRoleRequirement(UserRole.Administrator);
        var result = await _subject.Handle(requirement);

        // Assert
        Assert.AreEqual(true, result.IsAuthorized);
    }

    [TestMethod]
    public async Task WhenUserDoesNotHaveRole_Fails()
    {
        // Arrange
        _mockCurrentUserAdapter
            .Setup(m => m.IsInRole(It.IsAny<UserRole>()))
            .Returns(false);

        // Act
        var requirement = new MustHaveRoleRequirement(UserRole.Administrator);
        var result = await _subject.Handle(requirement);

        // Assert
        Assert.AreEqual(false, result.IsAuthorized);
    }
}
