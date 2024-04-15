using Core.Common;
using Core.Common.Database;
using Core.Music;
using Moq;

namespace Core.UnitTests.Music;

[TestClass]
public class DeleteAlbumCommandHandlerTests : HandlerTestsBase
{
    private Mock<DeleteAlbumCommand.IDatabasePort> _mockDatabaseAdapter = null!;
    private DeleteAlbumCommand.Handler _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDatabaseAdapter = new Mock<DeleteAlbumCommand.IDatabasePort>();
        _subject = new DeleteAlbumCommand.Handler(_mockDatabaseAdapter.Object);
    }

    [TestMethod]
    public async Task WhenAlbumDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbum(It.IsAny<int>()))
            .Returns(Task.FromResult((AlbumEntity?)null));

        // Act
        var request = new DeleteAlbumCommand(INCONSEQUENTIAL_ID);
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(action);
    }
}
