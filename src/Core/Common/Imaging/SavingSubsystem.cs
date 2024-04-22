using Core.Common.Database;
using SkiaSharp;

namespace Core.Common.Imaging;
internal class SavingSubsystem : IDisposable
{
    private readonly string _fileName;
    private readonly SKEncodedImageFormat _format;
    private readonly SKImage _originalImage;
    private readonly IFileSystemPort _fileSystemAdapter;

    public SavingSubsystem(IFileSystemPort fileSystemAdapter, string fileName, Stream stream, SKEncodedImageFormat format)
    {
        _fileSystemAdapter = fileSystemAdapter;
        _fileName = fileName;
        _originalImage = SKImage.FromEncodedData(stream);
        _format = format;
    }

    public void Dispose()
    {
        _originalImage.Dispose();
    }

    public async Task<ImageEntity> Execute()
    {
        return new ImageEntity
        {
            OriginalFile = await SaveFile(_originalImage),
            LargeFile = await SaveScaledIfLarger(_originalImage, ImageWidth.Large),
            MediumFile = await SaveScaledIfLarger(_originalImage, ImageWidth.Medium),
            ThumbnailFile = await SaveScaled(_originalImage, ImageWidth.Thumbnail),
        };
    }

    private async Task<FileEntity?> SaveScaledIfLarger(SKImage image, ImageWidth newWidth)
    {
        if (image.Width < newWidth.ToInt())
        {
            return null;
        }

        return await SaveScaled(image, newWidth);
    }

    private async Task<FileEntity> SaveScaled(SKImage image, ImageWidth newWidth)
    {
        var scaledWidth = newWidth.ToInt();
        var scaledHeight = image.Height * scaledWidth / image.Width;

        var scaledImageInfo = image.Info.WithSize(scaledWidth, scaledHeight);
        using var scaledImage = SKImage.Create(scaledImageInfo);
        image.ScalePixels(scaledImage.PeekPixels(), SKFilterQuality.High);

        return await SaveFile(scaledImage);
    }

    private async Task<FileEntity> SaveFile(SKImage image)
    {
        var serverFileName = $"{Guid.NewGuid()}.dat";

        using var inputStream = image.Encode(_format, 90).AsStream();
        await _fileSystemAdapter.SaveFile(serverFileName, inputStream);

        return new FileEntity
        {
            PublicFileName = _fileName,
            PrivateFileName = serverFileName,
        };
    }
}
