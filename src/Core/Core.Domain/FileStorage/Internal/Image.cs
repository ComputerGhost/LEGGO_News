using SkiaSharp;

namespace Core.Domain.FileStorage;
internal class Image : IDisposable
{
    private readonly SKBitmap _bitmap;
    private readonly string _fileName;
    private readonly SKEncodedImageFormat _format;

    public Image(Stream stream, string fileName)
    {
        _bitmap = SKBitmap.Decode(stream);
        _fileName = fileName;
        _format = GetImageFormat(Path.GetExtension(fileName));
    }

    private Image(SKBitmap bitmap, string fileName, SKEncodedImageFormat format)
    {
        _bitmap = bitmap;
        _fileName = fileName;
        _format = format;
    }

    public int Width => _bitmap.Width;
    public int Height => _bitmap.Height;
    public string FileName => _fileName;

    public void Dispose()
    {
        _bitmap?.Dispose();
    }

    public Image Resize(int newWidth)
    {
        var newHeight = _bitmap.Height * (newWidth / _bitmap.Width);
        var resized = _bitmap.Resize(new SKSizeI(newWidth, newHeight), SKFilterQuality.High);
        return new Image(resized, _fileName, _format);
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

    private SKEncodedImageFormat GetImageFormat(string extension)
    {
        return extension switch
        {
            "jpg" => SKEncodedImageFormat.Jpeg,
            "jpeg" => SKEncodedImageFormat.Jpeg,
            "png" => SKEncodedImageFormat.Png,
            "webp" => SKEncodedImageFormat.Webp,
            _ => throw new ArgumentException("Image file extension is not supported.")
        }; ;
    }
}
