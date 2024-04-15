using Core.Common;
using Core.Common.Database;
using Core.Music;
using Moq;

namespace Core.UnitTests.Music;

[TestClass]
public class GetAlbumQueryHandlerTests : HandlerTestsBase
{
    private Mock<GetAlbumQuery.IDatabasePort> _mockDatabaseAdapter = null!;
    private GetAlbumQuery.Handler _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDatabaseAdapter = new Mock<GetAlbumQuery.IDatabasePort>();
        _subject = new GetAlbumQuery.Handler(_mockDatabaseAdapter.Object);
    }

    [TestMethod]
    public async Task WhenAlbumDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbum(It.IsAny<int>()))
            .Returns(Task.FromResult<AlbumEntity?>(null));

        // Act
        var request = new GetAlbumQuery(INCONSEQUENTIAL_ID);
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(action);
    }

    [TestMethod]
    public async Task WhenAlbumExists_ReturnedDtoIsPopulated()
    {
        // Arrange
        var albumEntity = new AlbumEntity
        {
            Id = INCONSEQUENTIAL_ID,
            AlbumType = new AlbumTypeEntity { Name = AlbumType.Album.ToString() },
            Title = "title",
            Artist = "artist",
            ReleaseDate = INCONSEQUENTIAL_DATE,
            ImageId = INCONSEQUENTIAL_ID,
        };
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbum(It.IsAny<int>()))
            .Returns(Task.FromResult<AlbumEntity?>(albumEntity));

        // Act
        var request = new GetAlbumQuery(INCONSEQUENTIAL_ID);
        var result = await _subject.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual(albumEntity.Id, result.Id);
        Assert.AreEqual(albumEntity.AlbumType.Name, result.AlbumType.ToString());
        Assert.AreEqual(albumEntity.Title, result.Title);
        Assert.AreEqual(albumEntity.Artist, result.Artist);
        Assert.AreEqual(albumEntity.ReleaseDate, result.ReleaseDate);
        Assert.AreEqual(albumEntity.ImageId, result.AlbumArtImageId);
    }
}
