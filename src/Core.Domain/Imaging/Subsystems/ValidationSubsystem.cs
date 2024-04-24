using SkiaSharp;

namespace Core.Domain.Imaging.Subsystems;
internal class ValidationSubsystem
{
    private static string[] _supportedExtensions = [
        ".jpg",
        ".jpeg",
        ".png",
        ".webp",
    ];

    public bool CanLoadImage(Stream stream)
    {
        try
        {
            using var image = SKImage.FromEncodedData(stream);
            if (image != null)
            {
                return true;
            }
        }
        finally
        {
            stream.Seek(0, SeekOrigin.Begin);
        }

        return false;
    }

    public bool IsSupportedFileExtension(string extension)
    {
        return _supportedExtensions.Contains(extension);
    }
}
