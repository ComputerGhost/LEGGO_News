using Core.Application.Common.Models;
using Core.Application.Music;
using Core.Domain.Common.Entities;
using Core.Domain.Imaging.Ports;
using Core.Domain.Music.Ports;
using Moq;

namespace Core.Application.UnitTests.Music;

[TestClass]
public class CreateAlbumCommandHandlerTests : HandlerTestsBase
{
    private Mock<IMusicDatabasePort> _mockDatabaseAdapter = null!;
    private Mock<IFileSystemPort> _mockFileSystemAdapter = null!;
    private CreateAlbumCommand.Handler _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDatabaseAdapter = new();

        _mockFileSystemAdapter = new();
        _mockFileSystemAdapter
            .Setup(m => m.IsValidFileName(It.IsAny<string>()))
            .Returns(true);

        _subject = new(_mockDatabaseAdapter.Object, _mockFileSystemAdapter.Object);
    }

    [TestMethod]
    public async Task WhenFileSystemFails_DoesNotSaveToDatabase()
    {
        // Arrange
        _mockFileSystemAdapter
            .Setup(m => m.SaveFile(It.IsAny<string>(), It.IsAny<Stream>()))
            .Throws<Exception>();
        var request = new CreateAlbumCommand
        {
            AlbumArt = CreateGoodImageUpload(),
        };

        // Act
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<Exception>(action);
        _mockDatabaseAdapter.Verify(m => m.Create(It.IsAny<AlbumEntity>()), Times.Never());
    }

    [TestMethod]
    // This reinfoces the idea that the file must exist before saving to the database.
    // Of course it would be best to remove the file upon failure, but we won't do that immediately.
    public async Task WhenDatabaseFails_StillSavesToFileSystem()
    {
        // Arrange
        _mockDatabaseAdapter
            .Setup(m => m.Create(It.IsAny<AlbumEntity>()))
            .Throws<Exception>();
        var request = new CreateAlbumCommand
        {
            AlbumArt = CreateGoodImageUpload(),
        };

        // Act
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<Exception>(action);
        _mockFileSystemAdapter.Verify(m => m.SaveFile(It.IsAny<string>(), It.IsAny<Stream>()), Times.AtLeastOnce());
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
