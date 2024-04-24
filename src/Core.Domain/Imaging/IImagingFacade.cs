using Core.Domain.Common.Entities;
using SkiaSharp;

namespace Core.Domain.Imaging;
public interface IImagingFacade
{
    /// <summary>
    /// Deduces the image format from the file name.
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    SKEncodedImageFormat DeduceFormat(string fileName);

    /// <summary>
    /// Saves the image to the file system in various sizes.
    /// </summary>
    /// <remarks>
    /// The returned entity needs to be saved to the database.
    /// </remarks>
    Task<ImageEntity> SaveToFileSystem(string fileName, Stream stream);

    bool CanLoadImage(Stream stream);

    bool IsSupportedFileExtension(string extension);
}
