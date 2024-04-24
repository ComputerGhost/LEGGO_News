using SkiaSharp;

namespace Core.Domain.Imaging;
public static class ImageValidation
{
    public static bool CanLoadImage(Stream stream)
    {
        try
        {
            using var image = SKImage.FromEncodedData(stream);
            return image != null;
        }
        finally
        {
            stream.Seek(0, SeekOrigin.Begin);
        }
    }

    public static bool IsSupportedFileExtension(string extension)
    {
        string[] supportedExtensions = [".jpg", ".jpeg", ".png", ".webp",];
        return supportedExtensions.Contains(extension);
    }
}
