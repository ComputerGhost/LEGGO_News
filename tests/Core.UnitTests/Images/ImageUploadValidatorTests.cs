using Core.Images;

namespace Core.UnitTests.Images;

[TestClass]
public class ImageUploadValidatorTests : ValidatorTestsBase
{
    private static string[] ReservedFileNames => [
        "AUX", "CON", "NUL", "PRN",
        "COM0", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM¹", "COM²", "COM³",
        "LPT0", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9", "LPT¹", "LPT²", "LPT³"
    ];
    public static IEnumerable<object[]> ReservedFileNamesDataSource =>
        ReservedFileNames.Select(v => new[] { v });

    private ImageUpload.Validator _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _subject = new ImageUpload.Validator();
    }

    [TestMethod]
    public void WhenFileNameIsEmpty_Fails()
    {
        // Arrange
        var imageUpload = new ImageUpload { FileName = "", Stream = CreateGoodImageStream() };

        // Act
        var results = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(ImageUpload.FileName)));
    }

    [TestMethod]
    public void WhenFileNameIsTooLong_Fails()
    {
        // Arrange
        var fileName = ".jpg".PadLeft(256);
        var imageUpload = new ImageUpload { FileName = fileName, Stream = CreateGoodImageStream() };

        // Act
        var results = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(ImageUpload.FileName)));
    }

    [TestMethod]
    public void WhenFileNameHasInvalidCharacters_Fails()
    {
        // Arrange
        const string fileName = "../bad.jpg";
        var imageUpload = new ImageUpload { FileName = fileName, Stream = CreateGoodImageStream() };

        // Act
        var results = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(ImageUpload.FileName)));
    }

    [DataTestMethod]
    [DynamicData(nameof(ReservedFileNamesDataSource))]
    public void WhenFileNameIsReserved_Fails(string fileName)
    {
        // Arrange
        var imageUpload = new ImageUpload { FileName = fileName, Stream = CreateGoodImageStream() };

        // Act
        var results = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(ImageUpload.FileName)));
    }

    [TestMethod]
    public void WhenFileExtensionIsInvalid_Fails()
    {
        // Arrange
        const string fileName = "f.bad";
        var imageUpload = new ImageUpload { FileName = fileName, Stream = CreateGoodImageStream() };

        // Act
        var results = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(ImageUpload.FileName)));
    }

    [TestMethod]
    public void WhenStreamIsInvalid_Fails()
    {
        // Arrange
        var imageUpload = new ImageUpload { FileName = "f.jpg", Stream = new MemoryStream() };

        // Act
        var results = _subject.Validate(imageUpload);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(ImageUpload.Stream)));
    }

    [TestMethod]
    public void WhenEverythingIsValid_Passes()
    {
        // Arrange
        var imageUpload = new ImageUpload { FileName = "f.jpg", Stream = CreateGoodImageStream() };

        // Act
        var results = _subject.Validate(imageUpload);

        // Assert
        Assert.IsTrue(results.IsValid);
    }

    [TestMethod]
    public void WhenPasses_StreamPositionIsZero()
    {
        // Arrange
        var imageStream = CreateGoodImageStream();
        var imageUpload = new ImageUpload { FileName = "f.png", Stream = imageStream };

        // Act
        var results = _subject.Validate(imageUpload);

        // Assert
        Assert.IsTrue(results.IsValid);
        Assert.AreEqual(0, imageStream.Position);
    }
}
