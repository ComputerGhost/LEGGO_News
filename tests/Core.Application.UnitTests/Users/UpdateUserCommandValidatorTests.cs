using Core.Application.Users;

namespace Core.Application.UnitTests.Users;

[TestClass]
public class UpdateUserCommandValidatorTests : ValidatorTestsBase
{
    private UpdateUserCommand.Validator _subject;

    public UpdateUserCommandValidatorTests()
    {
        _subject = new();
    }

    [TestMethod]
    public void WhenEverythingIsValid_Passes()
    {
        // Arrange
        var request = CreateMinimalValidCommand();

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsTrue(results.IsValid);
    }

    [TestMethod]
    public void WhenIdentityIsEmpty_Fails()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        request.Identity = "";

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(UpdateUserCommand.Identity)));
    }

    [TestMethod]
    public void WhenIdentityIsTooLong_Fails()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        request.Identity = new string('-', 256);

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(UpdateUserCommand.Identity)));
    }

    [TestMethod]
    public void WhenDisplayNameIsEmpty_Fails()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        request.DisplayName = "";

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(UpdateUserCommand.DisplayName)));
    }

    [TestMethod]
    public void WhenDisplayNameIsTooLong_Fails()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        request.DisplayName = new string('-', 21);

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(UpdateUserCommand.DisplayName)));
    }

    private UpdateUserCommand CreateMinimalValidCommand()
    {
        return new UpdateUserCommand
        {
            Identity = "urn:uuid:00000000-0000-0000-0000-000000000000",
            DisplayName = "Null",
        };
    }
}
