namespace Core.ImageStorage;
internal interface IImageSaver
{
    Task<int> Create(string fileName, Stream stream);
}
