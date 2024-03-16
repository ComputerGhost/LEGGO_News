namespace Core.ImageStorage.Repositories;
internal interface IFileSystem
{
    Task Create(string partition, string fileName, Stream inputStream);
}
