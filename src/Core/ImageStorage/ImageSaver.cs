using Core.ImageStorage.Helpers;
using Core.ImageStorage.Repositories;
using Core.Startup;

namespace Core.ImageStorage;

[ServiceImplementation]
internal class ImageSaver : IImageSaver
{
    private readonly IFilesRepository _filesRepository;
    private readonly IFileSystem _fileSystem;
    private readonly IImagesRepository _imagesRepository;

    public ImageSaver(
        IFilesRepository filesRepository,
        IFileSystem fileSystem,
        IImagesRepository imagesRepository)
    {
        _filesRepository = filesRepository;
        _fileSystem = fileSystem;
        _imagesRepository = imagesRepository;
    }

    public async Task<int> Create(string fileName, Stream stream)
    {
        var originalId = await SaveOriginal(fileName, stream);

        using var originalImage = new Image(stream, Path.GetExtension(fileName));
        int? largeId = await SaveScaledIfLarger(fileName, originalImage, ImageWidth.Large);
        int? mediumId = await SaveScaledIfLarger(fileName, originalImage, ImageWidth.Medium);
        int thumbnailId = (await SaveScaledIfLarger(fileName, originalImage, ImageWidth.Thumbnail)).Value;

        return await _imagesRepository.Insert(originalId, largeId, mediumId, thumbnailId);
    }

    private Task<int> SaveOriginal(string fileName, Stream stream)
    {
        return SaveFile(fileName, stream);
    }

    private async Task<int?> SaveScaledIfLarger(string fileName, Image originalImage, ImageWidth newWidth)
    {
        if (originalImage.Width < newWidth.ToInt())
        {
            return null;
        }

        using var scaledImage = originalImage.Resize(newWidth);
        using var stream = scaledImage.ToStream();
        return await SaveFile(fileName, stream);
    }

    private async Task<int> SaveFile(string fileName, Stream stream)
    {
        var fileId = await _filesRepository.Insert(fileName);

        var savePath = $"images/f-{fileId}.dat";
        await _fileSystem.Create(savePath, stream);

        return fileId;
    }
}
