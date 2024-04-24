using Core.Application.Common.Models;
using Core.Application.Music;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Application.UnitTests.Music;

[TestClass]
public class UpdateAlbumCommandValidatorTests : ValidatorTestsBase
{
    private Mock<IValidator<ImageUpload>> _mockImageUploadValidator = null!;
    private UpdateAlbumCommand.Validator _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockImageUploadValidator = new();

        _subject = new(_mockImageUploadValidator.Object);
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
    public void WhenAlbumArtIsOmitted_AndEverythingElseIsValid_Passes()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        request.AlbumArt = null;

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsTrue(results.IsValid);
    }

    [TestMethod]
    public void WhenAlbumArtIsBad_Fails()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        _mockImageUploadValidator
            .Setup(m => m.Validate(It.IsAny<ValidationContext<ImageUpload>>()))
            .Callback((IValidationContext context) =>
            {
                var failure = new ValidationFailure(nameof(ImageUpload.FileName), "failure");
                ((ValidationContext<ImageUpload>)context).AddFailure(nameof(ImageUpload.FileName), "failure");
            });

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results,
            nameof(UpdateAlbumCommand.AlbumArt),
            nameof(UpdateAlbumCommand.AlbumArt.FileName)));
    }

    [TestMethod]
    public void WhenArtistIsEmpty_Fails()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        request.Artist = "";

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(UpdateAlbumCommand.Artist)));
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
        Assert.IsTrue(HasErrorForProperty(results, nameof(UpdateAlbumCommand.Artist)));
    }

    [TestMethod]
    public void WhenTitleIsEmpty_Fails()
    {
        // Arrange
        var request = CreateMinimalValidCommand();
        request.Title = "";

        // Act
        var results = _subject.Validate(request);

        // Assert
        Assert.IsFalse(results.IsValid);
        Assert.AreEqual(1, results.Errors.Count);
        Assert.IsTrue(HasErrorForProperty(results, nameof(UpdateAlbumCommand.Title)));
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
        Assert.IsTrue(HasErrorForProperty(results, nameof(UpdateAlbumCommand.Title)));
    }

    private UpdateAlbumCommand CreateMinimalValidCommand()
    {
        return new UpdateAlbumCommand
        {
            AlbumArt = new(),
        };
    }
}
