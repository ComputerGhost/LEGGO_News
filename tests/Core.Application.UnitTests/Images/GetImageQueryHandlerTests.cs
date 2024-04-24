using Core.Application.Common.Exceptions;
using Core.Application.Images;
using Core.Domain.Common.Entities;
using Core.Domain.Imaging.Enums;
using Core.Domain.Imaging.Ports;
using Moq;

namespace Core.Application.UnitTests.Images;

[TestClass]
public class GetImageQueryHandlerTests : HandlerTestsBase
{
    private Mock<IImagesDatabasePort> _mockDatabaseAdapter = null!;
    private Mock<IFileSystemPort> _mockFileSystemAdapter = null!;
    private GetImageQuery.Handler _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDatabaseAdapter = new();
        _mockFileSystemAdapter = new();
        _subject = new(_mockDatabaseAdapter.Object, _mockFileSystemAdapter.Object);
    }

    [TestMethod]
    public async Task WhenImageDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        SetFetchImageReturn(null);

        // Act
        var request = new GetImageQuery(INCONSEQUENTIAL_ID, ImageWidth.Original);
        var task = _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(() => task);
        _mockDatabaseAdapter.Verify(x => x.FetchFile(It.IsAny<int>()), Times.Never());
    }

    [DataTestMethod]
    [DataRow(ImageWidth.Medium)]
    [DataRow(ImageWidth.Large)]
    public async Task WhenImageSizeDoesNotExist_ThrowsNotFoundException(ImageWidth imageWidth)
    {
        // Arrange
        SetFetchImageReturn(new ImageEntity());

        // Act
        var request = new GetImageQuery(INCONSEQUENTIAL_ID, imageWidth);
        var action = () => _subject.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(action);
        _mockDatabaseAdapter.Verify(x => x.FetchFile(It.IsAny<int>()), Times.Never());
    }

    [DataTestMethod]
    [DataRow(ImageWidth.Original)]
    [DataRow(ImageWidth.Thumbnail)]
    public async Task WhenCompulsoryImageSizeRequested_AlwaysReturnsFile(ImageWidth imageWidth)
    {
        // Arrange
        SetFetchImageReturn(new ImageEntity());
        SetFetchFileReturn(new FileEntity { PublicFileName = "f.jpg", });
        SetLoadFileReturn(new MemoryStream());

        // Act
        var request = new GetImageQuery(INCONSEQUENTIAL_ID, imageWidth);
        var responseDto = await _subject.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(responseDto);
        Assert.AreEqual("f.jpg", responseDto.FileName);
        Assert.IsNotNull(responseDto.MimeType);
        Assert.IsNotNull(responseDto.Stream);
    }

    [DataTestMethod]
    [DataRow("f.jpg", "image/jpeg")]
    [DataRow("f.jpeg", "image/jpeg")]
    [DataRow("f.png", "image/png")]
    [DataRow("f.webp", "image/webp")]
    public async Task WhenImageReturned_IncludesCorrectMimeType(string fileName, string mimeType)
    {
        // Arrange
        SetFetchImageReturn(new ImageEntity());
        SetFetchFileReturn(new FileEntity { PublicFileName = fileName, });
        SetLoadFileReturn(new MemoryStream()); ;

        // Act
        var request = new GetImageQuery(INCONSEQUENTIAL_ID, ImageWidth.Original);
        var responseDto = await _subject.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(responseDto);
        Assert.AreEqual(mimeType, responseDto.MimeType);
    }

    private void SetFetchImageReturn(ImageEntity? imageEntity)
    {
        _mockDatabaseAdapter
            .Setup(m => m.FetchImage(It.IsAny<int>()))
            .Returns(Task.FromResult(imageEntity));
    }

    private void SetFetchFileReturn(FileEntity? fileEntity)
    {
        _mockDatabaseAdapter
            .Setup(m => m.FetchFile(It.IsAny<int>()))
            .Returns(Task.FromResult(fileEntity));
    }

    private void SetLoadFileReturn(Stream fileStream)
    {
        _mockFileSystemAdapter
            .Setup(m => m.LoadFile(It.IsAny<string>()))
            .Returns(Task.FromResult(fileStream));
    }
}
