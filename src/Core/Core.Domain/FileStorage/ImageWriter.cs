using Core.Domain.FileStorage.Ports;
using SkiaSharp;

namespace Core.Domain.FileStorage;
public class ImageWriter
{
    private readonly FileWriter _fileWriter;
    private readonly IImagesRepository _imagesRepository;

    public ImageWriter(IFilesRepository filesRepository, IFileSystem fileSystem, IImagesRepository imagesRepository)
    {
        _fileWriter = new FileWriter(filesRepository, fileSystem);
        _imagesRepository = imagesRepository;
    }

    public async Task<int> CreateImage(Stream stream, string fileName)
    {
        int originalId = await SaveOriginal(stream, fileName);

        using var original = new Image(stream, fileName);
        int? largeId = (original.Width >= 1024) ? await SaveScaled(original, 1024) : null;
        int? mediumId = (original.Width >= 300) ? await SaveScaled(original, 300) : null;
        int thumbnailId = await SaveScaled(original, 150);

        return await _imagesRepository.Insert(originalId, largeId, mediumId, thumbnailId);
    }

    private async Task<int> SaveOriginal(Stream stream, string fileName)
    {
        var originalId = await _fileWriter.CreateFile(stream, fileName);
        stream.Seek(0, SeekOrigin.Begin);
        return originalId;
    }

    private async Task<int> SaveScaled(Image original, int newWidth)
    {
        using var scaled = original.Resize(newWidth);
        using var stream = scaled.ToStream();
        return await _fileWriter.CreateFile(stream, scaled.FileName);
    }
}
