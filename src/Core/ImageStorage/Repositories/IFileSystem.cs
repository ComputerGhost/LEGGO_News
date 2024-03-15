namespace Core.ImageStorage.Repositories;
internal interface IFileSystem
{
    Task Create(string fileName, Stream inputStream);
}
