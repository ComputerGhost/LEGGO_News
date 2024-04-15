using Core.Images.Operations;

namespace Core.UnitTests.Images.Operations;

[TestClass]
public class FormattingSubsystemTests
{
    [TestMethod]
    public void WhenFileExtensionIsInvalid_ThrowsException()
    {
        // Arrange
        const string fileName = "f.bad";

        // Act
        var subject = new FormattingSubsystem(fileName);
        var action = () => { subject.Execute(); };

        // Assert
        Assert.ThrowsException<NotSupportedException>(action);
    }

    [DataTestMethod]
    [DataRow("f.jpeg")]
    [DataRow("f.jpg")]
    [DataRow("f.png")]
    [DataRow("f.webp")]
    public void WhenFileExtensionIsValid_DoesNotThrowException(string fileName)
    {
        // Arrange

        // Act
        var subject = new FormattingSubsystem(fileName);
        var action = subject.Execute;

        // Assert
        action.Invoke();
    }
}
