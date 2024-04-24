using Core.Application.Common.Exceptions;
using Core.Application.Music;
using Core.Domain.Common.Entities;
using Core.Domain.Imaging;
using Core.Domain.Music.Ports;
using Moq;

namespace Core.Application.UnitTests.Music;

[TestClass]
public class UpdateAlbumCommandHandlerTests
{
    private Mock<IImagingFacade> _mockImagingFacade = null!;
    private Mock<IMusicDatabasePort> _mockDatabaseAdapter = null!;
    private UpdateAlbumCommand.Handler _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockImagingFacade = new();

        _mockDatabaseAdapter = new();
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbumType(It.IsAny<string>()))
            .Returns(Task.FromResult(new AlbumTypeEntity()));

        _subject = new(_mockImagingFacade.Object, _mockDatabaseAdapter.Object);
    }

    [TestMethod]
    public async Task WhenAlbumDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbum(It.IsAny<int>()))
            .Returns(Task.FromResult<AlbumEntity?>(null));

        // Act
        var request = new UpdateAlbumCommand();
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(action);

    }

    [TestMethod]
    public async Task WhenFileSystemFails_DoesNotSaveToDatabase()
    {
        // Arrange
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbum(It.IsAny<int>()))
            .Returns(Task.FromResult<AlbumEntity?>(new()));
        _mockImagingFacade
            .Setup(m => m.SaveToFileSystem(It.IsAny<string>(), It.IsAny<Stream>()))
            .Throws<Exception>();

        // Act
        var request = new UpdateAlbumCommand { AlbumArt = new() };
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
            .Setup(m => m.FetchAlbum(It.IsAny<int>()))
            .Returns(Task.FromResult<AlbumEntity?>(new()));
        _mockDatabaseAdapter
            .Setup(m => m.Update(It.IsAny<AlbumEntity>()))
            .Throws<Exception>();

        // Act
        var request = new UpdateAlbumCommand { AlbumArt = new() };
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<Exception>(action);
        _mockImagingFacade.Verify(m => m.SaveToFileSystem(It.IsAny<string>(), It.IsAny<Stream>()), Times.Once());
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
        _mockImagingFacade
            .Setup(m => m.SaveToFileSystem(It.IsAny<string>(), It.IsAny<Stream>()))
            .Returns(Task.FromResult(new ImageEntity()));

        // Act
        var request = new UpdateAlbumCommand { AlbumArt = new() };
        await _subject.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreNotEqual(existingAlbumArt, existingAlbum.Image);
        _mockImagingFacade.Verify(m => m.SaveToFileSystem(It.IsAny<string>(), It.IsAny<Stream>()), Times.Once());
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
        _mockImagingFacade
            .Setup(m => m.SaveToFileSystem(It.IsAny<string>(), It.IsAny<Stream>()))
            .Returns(Task.FromResult(new ImageEntity()));

        // Act
        var request = new UpdateAlbumCommand { AlbumArt = null };
        await _subject.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual(existingAlbumArt, existingAlbum.Image);
        _mockImagingFacade.Verify(m => m.SaveToFileSystem(It.IsAny<string>(), It.IsAny<Stream>()), Times.Never());
    }
}
