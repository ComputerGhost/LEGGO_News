using Core.Domain.Common.Entities;
using Core.Domain.Imaging.Enums;
using Core.Domain.Imaging.Ports;
using SkiaSharp;

namespace Core.Domain.Imaging;
public class ImageSaver(IFileSystemPort fileSystemAdapter)
{
    /// <summary>
    /// Saves the image to the filesystem in various preset sizes.
    /// </summary>
    /// <remarks>
    /// The returned entity still needs to be saved to the database.
    /// </remarks>
    public Task<ImageEntity> SaveToFileSystem(string fileName, Stream stream)
    {
        var operation = new Operation(fileName, stream, fileSystemAdapter);
        return operation.Execute();
    }

    // This is a separate class so that the context can be class-scoped without interferring with other Save calls.
    private class Operation(string fileName, Stream stream, IFileSystemPort fileSystemAdapter)
    {
        public async Task<ImageEntity> Execute()
        {
            var originalImage = SKImage.FromEncodedData(stream);

            var originalTask = SaveFile(originalImage);
            var largeTask = SaveScaledIfLarger(originalImage, ImageWidth.Large);
            var mediumTask = SaveScaledIfLarger(originalImage, ImageWidth.Medium);
            var thumbnailTask = SaveScaled(originalImage, ImageWidth.Thumbnail);
            await Task.WhenAll(originalTask, largeTask!, mediumTask!, thumbnailTask);

            return new ImageEntity
            {
                OriginalFile = originalTask.Result,
                LargeFile = largeTask.Result,
                MediumFile = mediumTask.Result,
                ThumbnailFile = thumbnailTask.Result,
            };
        }

        private Task<FileEntity?> SaveScaledIfLarger(SKImage originalImage, ImageWidth newWidth)
        {
            if (originalImage.Width < newWidth.ToInt())
            {
                return Task.FromResult<FileEntity?>(null);
            }

            return SaveScaled(originalImage, newWidth)!;
        }

        private async Task<FileEntity> SaveScaled(SKImage originalImage, ImageWidth newWidth)
        {
            var scaledWidth = newWidth.ToInt();
            var scaledHeight = originalImage.Height * scaledWidth / originalImage.Width;

            var scaledImageInfo = originalImage.Info.WithSize(scaledWidth, scaledHeight);
            using var scaledImage = SKImage.Create(scaledImageInfo);
            originalImage.ScalePixels(scaledImage.PeekPixels(), SKFilterQuality.High);

            return await SaveFile(scaledImage);
        }

        private async Task<FileEntity> SaveFile(SKImage image)
        {
            var format = DeduceFormat();
            using var inputStream = image.Encode(format, 90).AsStream();

            var serverFileName = $"{Guid.NewGuid()}.dat";
            await fileSystemAdapter.SaveFile(serverFileName, inputStream);

            return new FileEntity
            {
                PublicFileName = fileName,
                PrivateFileName = serverFileName,
            };
        }

        private SKEncodedImageFormat DeduceFormat()
        {
            return Path.GetExtension(fileName) switch
            {
                ".jpg" => SKEncodedImageFormat.Jpeg,
                ".jpeg" => SKEncodedImageFormat.Jpeg,
                ".png" => SKEncodedImageFormat.Png,
                ".webp" => SKEncodedImageFormat.Webp,
                _ => throw new NotSupportedException($"The image file extension is not supported.")
            };
        }
    }
}
