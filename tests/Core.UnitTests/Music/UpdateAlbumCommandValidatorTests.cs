using Core.Common.Imaging;
using Core.Music;

namespace Core.UnitTests.Music;

[TestClass]
public class UpdateAlbumCommandValidatorTests : ValidatorTestsBase
{
    private UpdateAlbumCommand.Validator _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        var imageUploadValidator = new ImageUpload.Validator();
        _subject = new UpdateAlbumCommand.Validator(imageUploadValidator);
    }

    [TestMethod]
    public void WhenAlbumArtIsBad_Fails()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        request.AlbumArt = new ImageUpload
        {
            FileName = "",
            Stream = CreateGoodImageStream(),
        };

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
        Assert.IsFalse(HasErrorForProperty(results, nameof(CreateAlbumCommand.AlbumArt)));
        Assert.IsFalse(HasErrorForProperty(results, nameof(CreateAlbumCommand.AlbumType)));
        Assert.IsFalse(HasErrorForProperty(results, nameof(CreateAlbumCommand.Artist)));
        Assert.IsFalse(HasErrorForProperty(results, nameof(CreateAlbumCommand.ReleaseDate)));
        Assert.IsFalse(HasErrorForProperty(results, nameof(CreateAlbumCommand.Title)));
    }

    private UpdateAlbumCommand CreateMinimalValidCommand()
    {
        return new UpdateAlbumCommand
        {
            AlbumArt = new ImageUpload
            {
                FileName = "f.jpg",
                Stream = CreateGoodImageStream(),
            },
        };
    }
}
