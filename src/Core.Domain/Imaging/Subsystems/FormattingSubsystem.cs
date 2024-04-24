using SkiaSharp;

namespace Core.Domain.Imaging.Subsystems;
internal class FormattingSubsystem
{
    private readonly string _fileName;

    public FormattingSubsystem(string fileName)
    {
        _fileName = fileName;
    }

    public SKEncodedImageFormat Execute()
    {
        var extension = Path.GetExtension(_fileName);
        return extension switch
        {
            ".jpg" => SKEncodedImageFormat.Jpeg,
            ".jpeg" => SKEncodedImageFormat.Jpeg,
            ".png" => SKEncodedImageFormat.Png,
            ".webp" => SKEncodedImageFormat.Webp,
            _ => throw new NotSupportedException($"Image file extension '{extension}' is not supported.")
        };
    }
}
