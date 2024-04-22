using Core.Common.Imaging;
using Core.Music;

namespace Core.UnitTests.Music;

[TestClass]
public class CreateAlbumCommandValidatorTests : ValidatorTestsBase
{
    private CreateAlbumCommand.Validator _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        var imageUploadValidator = new ImageUpload.Validator();
        _subject = new CreateAlbumCommand.Validator(imageUploadValidator);
    }

    [TestMethod]
    public void WhenAlbumArtIsBad_Fails()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        request.AlbumArt.FileName = "";

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, 
            nameof(CreateAlbumCommand.AlbumArt),
            nameof(CreateAlbumCommand.AlbumArt.FileName)));
    }

    [TestMethod]
    public void WhenArtistIsTooLong_Fails()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        request.Artist = "long artist".PadLeft(51);

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(CreateAlbumCommand.Artist)));
    }

    [TestMethod]
    public void WhenTitleIsTooLong_Fails()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        request.Title = "long title".PadLeft(51);

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(CreateAlbumCommand.Title)));
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

    private CreateAlbumCommand CreateMinimalValidCommand()
    {
        return new CreateAlbumCommand
        {
            AlbumArt = new ImageUpload
            {
                FileName = "f.jpg",
                Stream = CreateGoodImageStream(),
            },
        };
    }
}
