using Core.ImageStorage;
using SkiaSharp;

namespace Core.Images;
internal class Image : IDisposable
{
    private readonly SKImage _image;

    public Image(Stream stream, string fileName)
    {
        // Quick sanity check of the filename's extension.
        var _ = DeduceFormat(fileName);

        _image = SKImage.FromEncodedData(stream);
        FileName = fileName;
    }

    private Image(SKImage image, string fileName)
    {
        _image = image;
        FileName = fileName;
    }

    public string FileName { get; init; }
    public int Width => _image.Width;
    public int Height => _image.Height;

    public void Dispose()
    {
        _image?.Dispose();
    }

    public Image Scale(ImageWidth width)
    {
        var scaledWidth = width.ToInt();
        var scaledHeight = _image.Height * scaledWidth / _image.Width;

        var scaledImageInfo = _image.Info.WithSize(scaledWidth, scaledHeight);
        var scaledImage = SKImage.Create(scaledImageInfo);
        _image.ScalePixels(scaledImage.PeekPixels(), SKFilterQuality.High);

        return new Image(scaledImage, FileName);
    }

    public Stream ToStream()
    {
        var format = DeduceFormat(FileName);
        return _image.Encode(format, 80).AsStream(true);
    }

    private static SKEncodedImageFormat DeduceFormat(string fileName)
    {
        var extension = Path.GetExtension(fileName);
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
