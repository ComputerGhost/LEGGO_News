using Core.Common;
using Core.Common.Database;
using Core.Images;
using Core.Images.Operations;
using Core.Music;
using Moq;

namespace Core.UnitTests.Music;

[TestClass]
public class UpdateAlbumCommandHandlerTests
{
    private Mock<IImagingFacade> _mockImagingFacade = null!;
    private Mock<UpdateAlbumCommand.IDatabasePort> _mockDatabaseAdapter = null!;
    private UpdateAlbumCommand.Handler _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockImagingFacade = new Mock<IImagingFacade>();

        _mockDatabaseAdapter = new Mock<UpdateAlbumCommand.IDatabasePort>();
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbumType(It.IsAny<string>()))
            .Returns(Task.FromResult(new AlbumTypeEntity()));

        _subject = new UpdateAlbumCommand.Handler(_mockImagingFacade.Object, _mockDatabaseAdapter.Object);
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
            .Returns(Task.FromResult<AlbumEntity?>(new AlbumEntity()));
        _mockImagingFacade
            .Setup(m => m.SaveToFileSystem(It.IsAny<ImageUpload>()))
            .Throws<Exception>();

        // Act
        var request = new UpdateAlbumCommand { AlbumArt = new ImageUpload() };
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<Exception>(action);
        _mockDatabaseAdapter.Verify(m => m.SaveChanges(), Times.Never());
    }

    [TestMethod]
    // This reinfoces the idea that the file must exist before saving to the database.
    // Of course it would be best to remove the file upon failure, but we won't do that immediately.
    public async Task WhenDatabaseFails_StillSavesAlbumArtToFileSystem()
    {
        // Arrange
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbum(It.IsAny<int>()))
            .Returns(Task.FromResult<AlbumEntity?>(new AlbumEntity()));
        _mockDatabaseAdapter
            .Setup(m => m.SaveChanges())
            .Throws<Exception>();

        // Act
        var request = new UpdateAlbumCommand { AlbumArt = new ImageUpload() };
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<Exception>(action);
        _mockImagingFacade.Verify(m => m.SaveToFileSystem(It.IsAny<ImageUpload>()), Times.Once());
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
            .Setup(m => m.SaveToFileSystem(It.IsAny<ImageUpload>()))
            .Returns(Task.FromResult(new ImageEntity()));

        // Act
        var request = new UpdateAlbumCommand { AlbumArt = new ImageUpload() };
        await _subject.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreNotEqual(existingAlbumArt, existingAlbum.Image);
        _mockImagingFacade.Verify(m => m.SaveToFileSystem(It.IsAny<ImageUpload>()), Times.Once());
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
            .Setup(m => m.SaveToFileSystem(It.IsAny<ImageUpload>()))
            .Returns(Task.FromResult(new ImageEntity()));

        // Act
        var request = new UpdateAlbumCommand { AlbumArt = null };
        await _subject.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual(existingAlbumArt, existingAlbum.Image);
        _mockImagingFacade.Verify(m => m.SaveToFileSystem(It.IsAny<ImageUpload>()), Times.Never());
    }
}
