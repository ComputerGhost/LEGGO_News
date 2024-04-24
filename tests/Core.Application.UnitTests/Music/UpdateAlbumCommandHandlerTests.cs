using Core.Application.Common.Exceptions;
using Core.Application.Common.Models;
using Core.Application.Music;
using Core.Domain.Common.Entities;
using Core.Domain.Imaging.Ports;
using Core.Domain.Music.Ports;
using Moq;

namespace Core.Application.UnitTests.Music;

[TestClass]
public class UpdateAlbumCommandHandlerTests : HandlerTestsBase
{
    private Mock<IMusicDatabasePort> _mockDatabaseAdapter = null!;
    private Mock<IFileSystemPort> _mockFileSystemAdapter = null!;
    private UpdateAlbumCommand.Handler _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockFileSystemAdapter = new();

        _mockDatabaseAdapter = new();
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbum(It.IsAny<int>()))
            .Returns(Task.FromResult<AlbumEntity?>(new()));

        _subject = new(_mockDatabaseAdapter.Object, _mockFileSystemAdapter.Object);
    }

    [TestMethod]
    public async Task WhenAlbumDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbum(It.IsAny<int>()))
            .Returns(Task.FromResult<AlbumEntity?>(null));
        var request = new UpdateAlbumCommand();

        // Act
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(action);

    }

    [TestMethod]
    public async Task WhenFileSystemFails_DoesNotSaveToDatabase()
    {
        // Arrange
        _mockFileSystemAdapter
            .Setup(m => m.SaveFile(It.IsAny<string>(), It.IsAny<Stream>()))
            .Throws<Exception>();
        var request = new UpdateAlbumCommand
        {
            AlbumArt = CreateGoodImageUpload(),
        };

        // Act
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<Exception>(action);
        _mockDatabaseAdapter.Verify(m => m.Update(It.IsAny<AlbumEntity>()), Times.Never());
    }

    [TestMethod]
    // This reinfoces the idea that the file must exist before saving to the database.
    // Of course it would be best to remove the file upon failure, but we won't do that immediately.
    public async Task WhenDatabaseFails_StillSavesAlbumArtToFileSystem()
    {
        // Arrange
        _mockDatabaseAdapter
            .Setup(m => m.Update(It.IsAny<AlbumEntity>()))
            .Throws<Exception>();
        var request = new UpdateAlbumCommand
        {
            AlbumArt = CreateGoodImageUpload(),
        };

        // Act
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<Exception>(action);
        _mockFileSystemAdapter.Verify(m => m.SaveFile(It.IsAny<string>(), It.IsAny<Stream>()), Times.AtLeastOnce());
    }

    [TestMethod]
    public async Task WhenAlbumArtSpecified_UpdatesAlbumArt()
    {
        // Arrange
        var existingAlbumArt = new ImageEntity();
        var existingAlbum = new AlbumEntity { Image = existingAlbumArt };
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbum(It.IsAny<int>()))
            .Returns(Task.FromResult<AlbumEntity?>(existingAlbum));
        var request = new UpdateAlbumCommand
        {
            AlbumArt = CreateGoodImageUpload(),
        };

        // Act
        await _subject.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreNotEqual(existingAlbumArt, existingAlbum.Image);
        _mockFileSystemAdapter.Verify(m => m.SaveFile(It.IsAny<string>(), It.IsAny<Stream>()), Times.AtLeastOnce());
    }

    [TestMethod]
    public async Task WhenAlbumArtOmitted_DoesNotUpdateAlbumArt()
    {
        // Arrange
        var existingAlbumArt = new ImageEntity();
        var existingAlbum = new AlbumEntity { Image = existingAlbumArt };
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbum(It.IsAny<int>()))
            .Returns(Task.FromResult<AlbumEntity?>(existingAlbum));

        // Act
        var request = new UpdateAlbumCommand { AlbumArt = null };
        await _subject.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual(existingAlbumArt, existingAlbum.Image);
        _mockFileSystemAdapter.Verify(m => m.SaveFile(It.IsAny<string>(), It.IsAny<Stream>()), Times.Never());
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
