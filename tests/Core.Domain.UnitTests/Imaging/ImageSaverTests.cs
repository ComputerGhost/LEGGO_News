using Core.Domain.Imaging;
using Core.Domain.Imaging.Enums;
using Core.Domain.Imaging.Ports;
using Moq;
using SkiaSharp;

namespace Core.Domain.UnitTests.Imaging;

[TestClass]
public class ImageSaverTests
{
    private ImageSaver _subject = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        var mockFileSystemAdapter = new Mock<IFileSystemPort>();
        mockFileSystemAdapter
            .Setup(m => m.SaveFile(It.IsAny<string>(), It.IsAny<Stream>()))
            .Returns(Task.CompletedTask);

        _subject = new(mockFileSystemAdapter.Object);
    }

    [TestMethod]
    public async Task WhenImageIsLarge_AllSizesCreated()
    {
        // Arrange
        using var stream = CreateImageStream(ImageWidth.Large.ToInt(), 100);

        // Act
        var result = await _subject.SaveToFileSystem("f.jpg", stream);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.OriginalFile);
        Assert.IsNotNull(result.LargeFile);
        Assert.IsNotNull(result.MediumFile);
        Assert.IsNotNull(result.ThumbnailFile);
    }

    [TestMethod]
    public async Task WhenImageIsMedium_OnlyLargeSizeIsOmitted()
    {
        // Arrange
        using var stream = CreateImageStream(ImageWidth.Medium.ToInt(), 100);

        // Act
        var result = await _subject.SaveToFileSystem("f.jpg", stream);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.OriginalFile);
        Assert.IsNull(result.LargeFile);
        Assert.IsNotNull(result.MediumFile);
        Assert.IsNotNull(result.ThumbnailFile);
    }

    [TestMethod]
    public async Task WhenImageIsThumbnail_OnlyLargeAndMediumSizesAreOmitted()
    {
        // Arrange
        using var stream = CreateImageStream(ImageWidth.Thumbnail.ToInt(), 100);

        // Act
        var result = await _subject.SaveToFileSystem("f.jpg", stream);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.OriginalFile);
        Assert.IsNull(result.LargeFile);
        Assert.IsNull(result.MediumFile);
        Assert.IsNotNull(result.ThumbnailFile);
    }

    [TestMethod]
    public async Task WhenImageIsSmallerThanThumbnail_ThumbnailAndOriginalAreStillCreated()
    {
        // Arrange
        using var stream = CreateImageStream(ImageWidth.Thumbnail.ToInt() - 1, 100);

        // Act
        var result = await _subject.SaveToFileSystem("f.jpg", stream);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.OriginalFile);
        Assert.IsNull(result.LargeFile);
        Assert.IsNull(result.MediumFile);
        Assert.IsNotNull(result.ThumbnailFile);
    }

    private Stream CreateImageStream(int width, int height)
    {
        var imageInfo = new SKImageInfo(width, height);
        var image = SKImage.Create(imageInfo);
        return image.Encode(SKEncodedImageFormat.Jpeg, 100).AsStream();
    }
}
