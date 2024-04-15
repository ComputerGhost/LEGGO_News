using Core.Common.Database;
using Core.Images;
using Core.Images.Operations;
using Core.Music;
using Moq;

namespace Core.UnitTests.Music;

[TestClass]
public class CreateAlbumCommandHandlerTests
{
    private Mock<IImagingFacade> _mockImagingFacade = null!;
    private Mock<CreateAlbumCommand.IDatabasePort> _mockDatabaseAdapter = null!;
    private CreateAlbumCommand.Handler _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockImagingFacade = new Mock<IImagingFacade>();

        _mockDatabaseAdapter = new Mock<CreateAlbumCommand.IDatabasePort>();
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbumType(It.IsAny<string>()))
            .Returns(Task.FromResult(new AlbumTypeEntity()));

        _subject = new CreateAlbumCommand.Handler(_mockImagingFacade.Object, _mockDatabaseAdapter.Object);
    }

    [TestMethod]
    public async Task WhenFileSystemFails_DoesNotSaveToDatabase()
    {
        // Arrange
        _mockImagingFacade
            .Setup(m => m.SaveToFileSystem(It.IsAny<ImageUpload>()))
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
        _mockImagingFacade.Verify(m => m.SaveToFileSystem(It.IsAny<ImageUpload>()), Times.Once());
    }
}
