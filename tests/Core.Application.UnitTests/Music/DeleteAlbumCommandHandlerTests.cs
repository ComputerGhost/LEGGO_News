using Core.Application.Common.Exceptions;
using Core.Application.Music;
using Core.Domain.Common.Entities;
using Core.Domain.Music.Ports;
using Moq;

namespace Core.Application.UnitTests.Music;

[TestClass]
public class DeleteAlbumCommandHandlerTests : HandlerTestsBase
{
    private Mock<IMusicDatabasePort> _mockDatabaseAdapter = null!;
    private DeleteAlbumCommand.Handler _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDatabaseAdapter = new();
        _subject = new(_mockDatabaseAdapter.Object);
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
