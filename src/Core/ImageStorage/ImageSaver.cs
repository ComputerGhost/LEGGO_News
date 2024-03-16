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
        using var originalImage = new Image(stream, Path.GetExtension(fileName));

        var originalId = await SaveFile(fileName, stream);
        int? largeId = await SaveScaledIfLarger(fileName, originalImage, ImageWidth.Large);
        int? mediumId = await SaveScaledIfLarger(fileName, originalImage, ImageWidth.Medium);
        int thumbnailId = await SaveScaled(fileName, originalImage, ImageWidth.Thumbnail);

        return await _imagesRepository.Insert(originalId, largeId, mediumId, thumbnailId);
    }

    private async Task<int?> SaveScaledIfLarger(string fileName, Image originalImage, ImageWidth newWidth)
    {
        if (originalImage.Width < newWidth.ToInt())
        {
            return null;
        }

        return await SaveScaled(fileName, originalImage, newWidth);
    }

    private async Task<int> SaveScaled(string fileName, Image originalImage, ImageWidth newWidth)
    {
        using var scaledImage = originalImage.Resize(newWidth);
        using var stream = scaledImage.ToStream();
        return await SaveFile(fileName, stream);
    }

    private async Task<int> SaveFile(string fileName, Stream stream)
    {
        var fileId = await _filesRepository.Insert("images", fileName);
        await _fileSystem.Create("images", "f-{fileId}.dat", stream);
        return fileId;
    }
}
