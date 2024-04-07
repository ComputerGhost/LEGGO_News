using Core.Common.Database;
using Core.ImageStorage;
using Core.Startup;
using Microsoft.Extensions.Options;

namespace Core.Images.Storage;

[ServiceImplementation]
internal class ImageSaver : IImageSaver
{
    private readonly string _storagePath = null!;

    public ImageSaver(IOptions<CoreOptions> options)
    {
        _storagePath = options.Value.FileStoragePath;
    }

    public async Task<ImageEntity> Create(ImageUpload imageUpload)
    {
        using var originalImage = new Image(imageUpload.Stream, imageUpload.FileName);

        return new ImageEntity
        {
            OriginalFile = await SaveFile(originalImage),
            LargeFile = await SaveScaledIfLarger(originalImage, ImageWidth.Large),
            MediumFile = await SaveScaledIfLarger(originalImage, ImageWidth.Medium),
            ThumbnailFile = await SaveScaled(originalImage, ImageWidth.Thumbnail),
        };
    }

    private async Task<FileEntity?> SaveScaledIfLarger(Image originalImage, ImageWidth newWidth)
    {
        if (originalImage.Width < newWidth.ToInt())
        {
            return null;
        }

        return await SaveScaled(originalImage, newWidth);
    }

    private async Task<FileEntity> SaveScaled(Image originalImage, ImageWidth newWidth)
    {
        using var scaledImage = originalImage.Scale(newWidth);
        return await SaveFile(scaledImage);
    }

    private async Task<FileEntity> SaveFile(Image image)
    {
        var serverFileName = $"{Guid.NewGuid()}.dat";

        using var inputStream = image.ToStream();
        using var outputStream = File.Create(Path.Combine(_storagePath, serverFileName));
        await inputStream.CopyToAsync(outputStream);

        return new FileEntity
        {
            PrivateFileName = serverFileName,
            PublicFileName = image.FileName,
        };
    }
}
