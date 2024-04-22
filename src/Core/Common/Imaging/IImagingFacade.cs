using Core.Common.Database;
using SkiaSharp;

namespace Core.Common.Imaging;
internal interface IImagingFacade
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
    /// The calling code should save the returned ImageEntity to the database.
    /// </remarks>
    Task<ImageEntity> SaveToFileSystem(ImageUpload imageUpload);
}
