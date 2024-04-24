using Core.Application.Music;
using Core.Domain.Common.Entities;
using Core.Domain.Music.Enums;
using Core.Domain.Music.Ports;
using Moq;

namespace Core.Application.UnitTests.Music;

[TestClass]
public class ListAlbumsQueryHandlerTests : HandlerTestsBase
{
    private Mock<IMusicDatabasePort> _mockDatabaseAdapter = null!;
    private ListAlbumsQuery.Handler _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDatabaseAdapter = new();
        _subject = new(_mockDatabaseAdapter.Object);
    }

    [TestMethod]
    public async Task WhenAlbumTypeFilterNotSet_DoesNotFilter()
    {
        // Arrange
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbumType(It.IsAny<string>()))
            .Returns(Task.FromResult(new AlbumTypeEntity()));

        // Act
        var request = new ListAlbumsQuery();
        var results = await _subject.Handle(request, CancellationToken.None);

        // Assert
        _mockDatabaseAdapter.Verify(m => m.ListAlbums(), Times.Once());
    }

    [TestMethod]
    public async Task WhenAlbumTypeFilterSet_FiltersByAlbumType()
    {
        // Arrange
        _mockDatabaseAdapter
            .Setup(m => m.FetchAlbumType(It.IsAny<string>()))
            .Returns(Task.FromResult(new AlbumTypeEntity()));

        // Act
        var request = new ListAlbumsQuery { AlbumType = AlbumType.Album };
        var results = await _subject.Handle(request, CancellationToken.None);

        // Assert
        _mockDatabaseAdapter.Verify(m => m.ListAlbums(It.IsAny<AlbumTypeEntity>()), Times.Once());
    }

    [TestMethod]
    public async Task WhenAlbumsExist_ReturnedDtosArePopulated()
    {
        // Arrange
        var albumEntity = new AlbumEntity {
            Id = INCONSEQUENTIAL_ID,
            AlbumType = new AlbumTypeEntity { Name = AlbumType.Album.ToString() },
            Title = "title",
            Artist = "artist",
            ReleaseDate = INCONSEQUENTIAL_DATE,
            ImageId = INCONSEQUENTIAL_ID,
        };
        _mockDatabaseAdapter
            .Setup(m => m.ListAlbums())
            .Returns(Task.FromResult<IEnumerable<AlbumEntity>>([albumEntity]));

        // Act
        var request = new ListAlbumsQuery();
        var results = await _subject.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual(1, results.Count());
        var result = results.First();
        Assert.AreEqual(albumEntity.Id, result.Id);
        Assert.AreEqual(albumEntity.AlbumType.Name, result.AlbumType.ToString());
        Assert.AreEqual(albumEntity.Title, result.Title);
        Assert.AreEqual(albumEntity.Artist, result.Artist);
        Assert.AreEqual(albumEntity.ReleaseDate, result.ReleaseDate);
        Assert.AreEqual(albumEntity.ImageId, result.AlbumArtImageId);
    }
}
