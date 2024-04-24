using Core.Application.Music;
using Core.Domain.Common.Entities;
using Core.Domain.Imaging;
using Core.Domain.Music.Ports;
using Moq;

namespace Core.Application.UnitTests.Music;

[TestClass]
internal class CreateAlbumCommandHandlerTests : HandlerTestsBase
{
    private Mock<IImagingFacade> _mockImagingFacade = null!;
    private Mock<IMusicDatabasePort> _mockDatabaseAdapter = null!;
    private CreateAlbumCommand.Handler _subject = null!;

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
    public async Task WhenFileSystemFails_DoesNotSaveToDatabase()
    {
        // Arrange
        _mockImagingFacade
            .Setup(m => m.SaveToFileSystem(It.IsAny<string>(), It.IsAny<Stream>()))
            .Throws<Exception>();

        // Act
        var request = new CreateAlbumCommand();
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

        // Act
        var request = new CreateAlbumCommand();
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<Exception>(action);
        _mockImagingFacade.Verify(m => m.SaveToFileSystem(It.IsAny<string>(), It.IsAny<Stream>()), Times.Once());
    }
}
