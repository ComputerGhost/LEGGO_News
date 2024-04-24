using Core.Domain.Imaging;

namespace Core.Domain.UnitTests.Imaging;

[TestClass]
public class ImageValidationTests
{
    [TestMethod]
    public void CanLoadImage_WhenValidImage_ReturnsTrue()
    {
        // Arrange
        using var stream = CreateGoodImageStream();

        // Act
        var result = ImageValidation.CanLoadImage(stream);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanLoadImage_WhenInvalidImage_ReturnsFalse()
    {
        // Arrange
        using var stream = CreateBadImageStream();

        // Act
        var result = ImageValidation.CanLoadImage(stream);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CanLoadImage_WhenPasses_StreamPositionIsZero()
    {
        // Arrange
        using var stream = CreateGoodImageStream();

        // Act
        ImageValidation.CanLoadImage(stream);

        // Assert
        Assert.AreEqual(0, stream.Position);
    }

    [DataTestMethod]
    [DataRow(".jpg")]
    [DataRow(".jpeg")]
    [DataRow(".png")]
    [DataRow(".webp")]
    public void IsSupportedFileExtension_WhenSupported_ReturnsTrue(string extension)
    {
        // Arrange

        // Act
        var result = ImageValidation.IsSupportedFileExtension(extension);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsSupportedFileExtension_WhenUnsupported_ReturnsFalse()
    {
        // Arrange
        const string extension = ".bad";

        // Act
        var result = ImageValidation.IsSupportedFileExtension(extension);

        // Assert
        Assert.IsFalse(result);
    }

    private Stream CreateBadImageStream()
    {
        return new MemoryStream();
    }

    private Stream CreateGoodImageStream()
    {
        const string pngData_base64 = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVQYV2NgYAAAAAMAAWgmWQ0AAAAASUVORK5CYII=";
        var pngData = Convert.FromBase64String(pngData_base64);
        return new MemoryStream(pngData);
    }
}
