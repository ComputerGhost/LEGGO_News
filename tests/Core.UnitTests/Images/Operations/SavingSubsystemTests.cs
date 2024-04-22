using Core.Common.Imaging;
using Moq;
using SkiaSharp;

namespace Core.UnitTests.Images.Operations;

[TestClass]
public class SavingSubsystemTests
{
    [TestMethod]
    public async Task WhenImageIsLarge_AllSizesCreated()
    {
        // Arrange
        using var stream = CreateImageStream(ImageWidth.Large.ToInt(), 100);
        var subject = CreateSubject(stream);

        // Act
        var result = await subject.Execute();

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
        var subject = CreateSubject(stream);

        // Act
        var result = await subject.Execute();

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
        var subject = CreateSubject(stream);

        // Act
        var result = await subject.Execute();

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
        var subject = CreateSubject(stream);

        // Act
        var result = await subject.Execute();

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

    private SavingSubsystem CreateSubject(Stream stream)
    {
        var mockFileSystemAdapter = new Mock<IFileSystemPort>();
        mockFileSystemAdapter
            .Setup(m => m.SaveFile(It.IsAny<string>(), It.IsAny<Stream>()))
            .Returns(Task.CompletedTask);

        return new SavingSubsystem(mockFileSystemAdapter.Object, "f.jpg", stream, SKEncodedImageFormat.Jpeg);
    }
}
