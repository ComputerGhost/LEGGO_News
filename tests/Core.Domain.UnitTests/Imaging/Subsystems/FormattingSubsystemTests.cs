using Core.Domain.Imaging.Subsystems;

namespace Core.Domain.UnitTests.Imaging.Subsystems;

[TestClass]
public class FormattingSubsystemTests
{
    [TestMethod]
    public void WhenFileExtensionIsInvalid_ThrowsException()
    {
        // Arrange
        const string fileName = "f.bad";
        var subject = new FormattingSubsystem(fileName);

        // Act
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
        var subject = new FormattingSubsystem(fileName);

        // Act
        var action = subject.Execute;

        // Assert
        action.Invoke();
    }
}
