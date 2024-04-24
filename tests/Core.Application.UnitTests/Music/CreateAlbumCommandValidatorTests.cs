using Core.Application.Common.Models;
using Core.Application.Music;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Core.Application.UnitTests.Music;

[TestClass]
public class CreateAlbumCommandValidatorTests : ValidatorTestsBase
{
    private Mock<IValidator<ImageUpload>> _mockImageUploadValidator = null!;
    private CreateAlbumCommand.Validator _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockImageUploadValidator = new();
        _mockImageUploadValidator
            .Setup(m => m.Validate(It.IsAny<ImageUpload>()))
            .Returns(new ValidationResult());

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
            nameof(CreateAlbumCommand.AlbumArt),
            nameof(CreateAlbumCommand.AlbumArt.FileName)));
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
        Assert.IsTrue(HasErrorForProperty(results, nameof(CreateAlbumCommand.Artist)));
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
        Assert.IsTrue(HasErrorForProperty(results, nameof(CreateAlbumCommand.Title)));
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

    private CreateAlbumCommand CreateMinimalValidCommand()
    {
        return new CreateAlbumCommand
        {
            AlbumArt = new(),
        };
    }
}
