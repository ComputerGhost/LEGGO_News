using SkiaSharp;

namespace Core.ImageStorage.Helpers;

// Wraps the image processing library.
internal class Image : IDisposable
{
    private readonly SKBitmap _bitmap;
    private readonly SKEncodedImageFormat _format;

    public Image(Stream stream, string extension)
    {
        _bitmap = SKBitmap.Decode(stream);
        _format = DeduceFormat(extension);
    }

    private Image(SKBitmap bitmap, SKEncodedImageFormat format)
    {
        _bitmap = bitmap;
        _format = format;
    }

    public int Width => _bitmap.Width;
    public int Height => _bitmap.Height;

    public void Dispose()
    {
        _bitmap?.Dispose();
    }

    public Image Resize(ImageWidth newWidth)
    {
        var width = newWidth.ToInt();
        var height = _bitmap.Height * (width / _bitmap.Width);
        var scaledBitmap = _bitmap.Resize(new SKSizeI(width, height), SKFilterQuality.High);
        return new Image(scaledBitmap, _format);
    }

    public Stream ToStream()
    {
        using var image = SKImage.FromBitmap(_bitmap);
        using var data = image.Encode(_format, 80);
        var stream = new MemoryStream();
        data.SaveTo(stream);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    private static SKEncodedImageFormat DeduceFormat(string extension)
    {
        return extension switch
        {
            "jpg" => SKEncodedImageFormat.Jpeg,
            "jpeg" => SKEncodedImageFormat.Jpeg,
            "png" => SKEncodedImageFormat.Png,
            "webp" => SKEncodedImageFormat.Webp,
            _ => throw new ArgumentException("Image file extension is not supported.")
        };
    }
}
