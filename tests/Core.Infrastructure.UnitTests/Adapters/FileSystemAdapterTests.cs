using Core.Infrastructure.Adapters;
using Core.Infrastructure.Startup;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.UnitTests.Adapters;

[TestClass]
public class FileSystemAdapterTests
{
    public static IEnumerable<object[]> ReservedFileNamesDataSource => new[] {
        "AUX", "CON", "NUL", "PRN",
        "COM0", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM¹", "COM²", "COM³",
        "LPT0", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9", "LPT¹", "LPT²", "LPT³",
        }.Select(x => new[] { x });

    private FileSystemAdapter _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        var options = new InfrastructureOptions();
        var wrapper = new OptionsWrapper<InfrastructureOptions>(options);
        _subject = new(wrapper);
    }

    [TestMethod]
    public void IsValidFileName_WhenValid_ReturnsTrue()
    {
        // Arrange
        const string fileName = "good.jpg";

        // Act
        var result = _subject.IsValidFileName(fileName);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValidFileName_WhenEmpty_ReturnsFalse()
    {
        // Arrange
        const string fileName = "";

        // Act
        var result = _subject.IsValidFileName(fileName);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidFileName_WhenTooLong_ReturnsFalse()
    {
        // Arrange
        var fileName = ".jpg".PadLeft(256, 'f');

        // Act
        var result = _subject.IsValidFileName(fileName);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidFileName_WhenWhiteSpace_ReturnsFalse()
    {
        // Arrange
        var fileName = " ";

        // Act
        var result = _subject.IsValidFileName(fileName);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidFileName_WhenHasInvalidCharacters_ReturnsFalse()
    {
        // Arrange
        const string fileName = "../bad.jpg";

        // Act
        var result = _subject.IsValidFileName(fileName);

        // Assert
        Assert.IsFalse(result);
    }

    [DataTestMethod]
    [DynamicData(nameof(ReservedFileNamesDataSource))]
    public void IsValidFileName_WhenReserved_ReturnsFalse(string fileName)
    {
        // Arrange

        // Act
        var result = _subject.IsValidFileName(fileName);

        // Assert
        Assert.IsFalse(result);
    }
}
