using Core.Application.Common.Models;
using Core.Domain.Imaging;
using Core.Domain.Imaging.Ports;
using Moq;

namespace Core.Application.UnitTests.Common.Models;

[TestClass]
public class ImageUploadValidatorTests : ValidatorTestsBase
{
    private Mock<IFileSystemPort> _mockFileSystemAdapter = null!;
    private Mock<IImagingFacade> _mockImagingFacade = null!;
    private ImageUpload.Validator _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockFileSystemAdapter = new();
        _mockFileSystemAdapter.Setup(m => m.IsValidFileName(It.IsAny<string>())).Returns(true);

        _mockImagingFacade = new();
        _mockImagingFacade.Setup(m => m.CanLoadImage(It.IsAny<Stream>())).Returns(true);
        _mockImagingFacade.Setup(m => m.IsSupportedFileExtension(It.IsAny<string>())).Returns(true);

        _subject = new(_mockFileSystemAdapter.Object, _mockImagingFacade.Object);
    }

    [TestMethod]
    public void WhenAllGood_Passes()
    {
        // Arrange
        _mockFileSystemAdapter.Setup(m => m.IsValidFileName(It.IsAny<string>())).Returns(true);
        _mockImagingFacade.Setup(m => m.CanLoadImage(It.IsAny<Stream>())).Returns(true);
        _mockImagingFacade.Setup(m => m.IsSupportedFileExtension(It.IsAny<string>())).Returns(true);
        var imageUpload = new ImageUpload
        {
            FileName = "f.jpg",
            Stream = CreateGoodImageStream(),
        };

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
        var imageUpload = new ImageUpload
        {
            FileName = "",
            Stream = CreateGoodImageStream(),
        };

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
        var imageUpload = new ImageUpload
        {
            FileName = ".jpg".PadLeft(256),
            Stream = CreateGoodImageStream(),
        };

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
        _mockFileSystemAdapter.Setup(m => m.IsValidFileName(It.IsAny<string>())).Returns(false);
        var imageUpload = new ImageUpload
        {
            FileName = "bad.jpg",
            Stream = CreateGoodImageStream(),
        };

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
        const string BAD_EXTENSION = ".bad";
        _mockImagingFacade.Setup(m => m.IsSupportedFileExtension(It.IsAny<string>())).Returns(false);
        var imageUpload = new ImageUpload
        {
            FileName = $"f{BAD_EXTENSION}",
            Stream = CreateGoodImageStream(),
        };

        // Act
        var result = _subject.Validate(imageUpload);

        // Assert
        _mockImagingFacade.Verify(m => m.IsSupportedFileExtension(It.Is<string>(v => v == BAD_EXTENSION)));
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(1, result.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(result, nameof(ImageUpload.FileName)));
    }

    [TestMethod]
    public void WhenImageCannotLoad_Fails()
    {
        // Arrange
        _mockImagingFacade.Setup(m => m.CanLoadImage(It.IsAny<Stream>())).Returns(false);
        var imageUpload = new ImageUpload
        {
            FileName = "f.jpg",
            Stream = new MemoryStream(),
        };

        // Act
        var result = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(1, result.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(result, nameof(ImageUpload.Stream)));
    }
}
