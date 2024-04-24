using Core.Application.Common.Models;
using Core.Domain.Imaging.Ports;
using Moq;

namespace Core.Application.UnitTests.Common.Models;

[TestClass]
public class ImageUploadValidatorTests : ValidatorTestsBase
{
    private Mock<IFileSystemPort> _mockFileSystemAdapter = null!;
    private ImageUpload.Validator _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockFileSystemAdapter = new();
        _mockFileSystemAdapter.Setup(m => m.IsValidFileName(It.IsAny<string>())).Returns(true);

        _subject = new(_mockFileSystemAdapter.Object);
    }

    [TestMethod]
    public void WhenAllGood_Passes()
    {
        // Arrange
        _mockFileSystemAdapter.Setup(m => m.IsValidFileName(It.IsAny<string>())).Returns(true);
        var imageUpload = CreateGoodImageUpload();

        // Act
        var result = _subject.Validate(imageUpload);

        // Assert
        Assert.IsTrue(result.IsValid);
        Assert.AreEqual(0, result.Errors.Count);
    }

    [TestMethod]
    public void WhenFileNameIsEmpty_Fails()
    {
        // Arrange
        var imageUpload = CreateGoodImageUpload();
        imageUpload.FileName = "";

        // Act
        var result = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(1, result.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(result, nameof(ImageUpload.FileName)));
    }

    [TestMethod]
    public void WhenFileNameIsTooLong_Fails()
    {
        // Arrange
        var imageUpload = CreateGoodImageUpload();
        imageUpload.FileName = ".jpg".PadLeft(256);

        // Act
        var result = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(1, result.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(result, nameof(ImageUpload.FileName)));
    }

    [TestMethod]
    public void WhenFileNameIsInvalid_Fails()
    {
        // Arrange
        var imageUpload = CreateGoodImageUpload();
        imageUpload.FileName = "../bad";

        // Act
        var result = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(1, result.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(result, nameof(ImageUpload.FileName)));
    }

    [TestMethod]
    public void WhenFileExtensionIsUnsupported_Fails()
    {
        // Arrange
        var imageUpload = CreateGoodImageUpload();
        imageUpload.FileName = $"f.bad";

        // Act
        var result = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(1, result.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(result, nameof(ImageUpload.FileName)));
    }

    [TestMethod]
    public void WhenImageCannotLoad_Fails()
    {
        // Arrange
        var imageUpload = CreateGoodImageUpload();
        imageUpload.Stream?.Dispose();
        imageUpload.Stream = new MemoryStream();

        // Act
        var result = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(1, result.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(result, nameof(ImageUpload.Stream)));
    }

    private ImageUpload CreateGoodImageUpload()
    {
        return new ImageUpload
        {
            FileName = "good.jpg",
            Stream = CreateGoodImageStream(),
        };
    }
}
