namespace Core.Domain.FileStorage.Ports;
public interface IFileSystem
{
    Task Create(Stream inputStream, int fileId);
}
